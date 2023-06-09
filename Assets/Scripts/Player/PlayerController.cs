using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    #region Variables
    private PlayerAnimation anim;
    private PlayerCollision coll;
    private CharacterController controller;
    private InputActions playerControlls;
    private InputActionReference CatchingFood, GetingFood;

    [Header("Player Stats")]
    [Space]
    [SerializeField]
    private float playerSpeed;
    [SerializeField]
    private Vector3 playerVelocity;
    [SerializeField]
    private bool groundedPlayer;
    private float gravityValue = -9.81f;
    private Vector3 movement = Vector3.zero;
    public Vector3 _movement => this.movement;

    [Space]
    [Space]

    [Header("Debug")]
    [Space]
    [SerializeField]
    private GameObject debug;

    [Space]

    [Header("Food")]
    [Space]

    [Space]
    [Header("Bools")]
    [SerializeField]
    private bool isFood;
    [SerializeField]
    private bool isFoodBox;
    [SerializeField]
    private bool isCounter;
    [SerializeField]
    private bool isCounterInteractable;
    [SerializeField]
    private bool foodCatched = false;
    [SerializeField]
    private bool foodInside = false;
    [SerializeField]
    private bool foodOnCounter;

    [Space]
    [Header("GameObjects")]
    [SerializeField]
    private GameObject knifeGo;
    [SerializeField]
    private GameObject foodPrefab;
    [SerializeField]
    private GameObject foodPoint;
    [SerializeField]
    private GameObject foodPointOnCounter;
    [SerializeField]
    private GameObject foodGo;
    [SerializeField]
    private GameObject dishGo;

    [SerializeField]
    private GameObject foodInstance;
    [SerializeField]
    private GameObject dishInstance;
    [SerializeField]
    private GameObject counter;

    #endregion

    private void Awake()
    {
        playerControlls = new InputActions();
    }

    private void Start()
    {
        groundedPlayer = true;
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<PlayerAnimation>();
        coll = GetComponent<PlayerCollision>();
    }

    private void Update()
    {
        counter = coll._counter;
        foodGo = coll._foodGo;
        isFood = coll._isFood;
        isCounter = coll._isCounter;
        isCounterInteractable = coll._isCounterInteractable;
        isFoodBox = coll._isFoodBox;

        DebugInText("isFood: " + isFood + "\nisFoodBox: " + isFoodBox + "\nfoodCatched: " + foodCatched + "\nisCounter: " + isCounter + "\nisCounterInteractable: " + isCounterInteractable);

        if (groundedPlayer)
        {
            Move();
        }
    }

    private void DebugInText(string text)
    {
        debug.GetComponent<Text>().text = text;
    }

    private void Move()
    {
        // groundedPlayer = controller.isGrounded;
        // if (groundedPlayer && playerVelocity.y < 0)
        // {
        //     playerVelocity.y += gravityValue * Time.deltaTime;
        // }

        Vector3 rawMovement = playerControlls.Player.Movement.ReadValue<Vector2>();

        movement.x = rawMovement.x;
        movement.z = rawMovement.y;

        controller.Move(movement * Time.deltaTime * playerSpeed);

        if (movement != Vector3.zero)
        {
            gameObject.transform.forward = movement;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    private void CatchDish()
    {
        Debug.Log("CatchDish");
        if (dishGo == null)
        {
            dishGo = dishInstance;
        }

        anim.SetBool("isCatching", true);
        dishGo.transform.SetParent(foodPoint.transform);
    }

    private void CatchFood()
    {
        // Debug.Log("CatchFood");
        if (foodGo == null)
        {
            foodGo = foodInstance;
        }
        anim.SetBool("isCatching", true);

        foodGo.transform.SetParent(foodPoint.transform);
        foodGo.transform.position = foodPoint.transform.position;
        foodGo.GetComponent<Rigidbody>().isKinematic = true;
        foodGo.GetComponent<Collider>().enabled = false;
        foodCatched = true;
    }

    private void DropFood()
    {
        if (foodGo == null)
        {
            foodGo = foodInstance;
        }
        anim.SetBool("isCatching", false);

        foodGo.transform.SetParent(null);
        foodGo.transform.position = transform.position + transform.forward * 2f;
        foodGo.GetComponent<Rigidbody>().isKinematic = false;
        foodGo.GetComponent<Collider>().enabled = true;
        foodCatched = false;
    }

    public void DropOnCounter(GameObject foodOnPlayer)
    {
        anim.SetBool("isCatching", false);

        foodPointOnCounter = counter.transform.Find("FoodPoint").gameObject;

        foodOnPlayer.transform.SetParent(foodPointOnCounter.transform);
        foodOnPlayer.transform.position = foodPointOnCounter.transform.position;

        foodOnPlayer.GetComponent<Rigidbody>().isKinematic = true;
        foodOnPlayer.GetComponent<Collider>().enabled = false;

        foodInside = true;
        foodCatched = false;
        Invoke("CutFoodStart", 0.5f);
    }
    private void CutFoodStart()
    {
        foodOnCounter = true;
    }

    private void CutFoodEnd()
    {
        groundedPlayer = true;
        knifeGo.SetActive(false);
    }

    public void CutFood()
    {
        groundedPlayer = false;
        anim.SetBool("cut", true);
        knifeGo.SetActive(true);
        Invoke("CutFoodEnd", 1.5f);
    }

    public void OnGetingFood(InputAction.CallbackContext context)
    {
        // Debug.Log("GetFood");
        if (context.started && isFoodBox)
        {
            counter.transform.Find("Visual").gameObject.GetComponent<CounterAnimation>().SetTrigger("open");
            foodInstance = Instantiate(foodPrefab, foodPoint.transform.position, Quaternion.identity);
            Invoke("CatchFood", 0.1f);
            foodGo = null;
        }
    }

    public void OnCuttingFood(InputAction.CallbackContext context)
    {
        if (context.started && foodOnCounter && isCounterInteractable)
        {
            CutFood();
        }
    }

    public void OnCatchingFood(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (isFood && !foodCatched)
            {
                // Debug.Log("Catch started");
                CatchFood();
            }
            else if (foodCatched)
            {
                if (isCounter || isCounterInteractable)
                {
                    if (!foodInside)
                        DropOnCounter(foodInstance);
                }
                else
                {
                    DropFood();
                }
            }
        }
    }

    private void OnEnable()
    {
        playerControlls.Player.Enable();
    }

    private void OnDisable()
    {
        playerControlls.Player.Disable();
    }
}
