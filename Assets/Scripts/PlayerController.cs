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
    [SerializeField]
    private float playerSpeed;
    [SerializeField]
    private Vector3 playerVelocity;
    [SerializeField]
    private float gravityValue = -9.81f;
    private bool groundedPlayer;
    public Vector3 movement = Vector3.zero;
    public Vector3 _movement => this.movement;

    [Space]

    [Header("Food")]
    [SerializeField]
    private GameObject foodPrefab;
    [SerializeField]
    private GameObject foodPoint;

    [SerializeField]
    private bool isFood;
    [SerializeField]
    private bool isFoodBox;
    [SerializeField]
    private bool foodCatched = false;
    [SerializeField]
    private GameObject foodGo;

    [SerializeField]
    private GameObject foodInstance;

    [Space]
    [SerializeField]
    private GameObject debug;



    #endregion

    private void Awake()
    {
        playerControlls = new InputActions();
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<PlayerAnimation>();
        coll = GetComponent<PlayerCollision>();
    }

    private void Update()
    {
        foodGo = coll.foodGo;
        isFood = coll._isFood;
        isFoodBox = coll._isFoodBox;
        Move();
        DebugInText("isFood: " + isFood + "\nisFoodBox: " + isFoodBox + "\nfoodCatched: " + foodCatched);        
    }

    private void DebugInText(string text)
    {
        debug.GetComponent<Text>().text = text;
    }

    private void CatchFood()
    {
        anim.SetBool("isCatching", true);

        foodGo.transform.SetParent(foodPoint.transform);
        foodGo.transform.position = foodPoint.transform.position;
        foodGo.GetComponent<Rigidbody>().isKinematic = true;
        foodCatched = true;
    }

    private void DropFood()
    {
        anim.SetBool("isCatching", false);

        foodGo.transform.SetParent(null);
        foodGo.transform.position = transform.position + transform.forward * 2f;
        foodGo.GetComponent<Rigidbody>().isKinematic = false;
        foodCatched = false;
    }

    private void Move()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

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

    public void OnGetingFood(InputAction.CallbackContext context)
    {
        // Debug.Log("GetFood");
        if (context.started && isFoodBox)
        {
            foodInstance = Instantiate(foodPrefab, foodPoint.transform.position, Quaternion.identity);
            Invoke("CatchFood", 0.1f);
        }
    }

    public void OnCatchingFood(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // Debug.Log("Catch started");
            if (isFood && !foodCatched)
                CatchFood();
            else if (foodCatched)
                DropFood();
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
