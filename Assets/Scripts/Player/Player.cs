using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    #region Variables
    private PlayerAnimation anim;
    private PlayerCollision coll;
    [SerializeField]
    private GameInput gameInput;

    [Header("Debug")]
    [Space]
    [SerializeField]
    private GameObject debug;

    [Space]
    [Header("Stats")]
    private bool isWalking;

    [Header("Food")]
    [Space]

    [Space]
    [Header("Bools")]
    [SerializeField] private bool boxDetect;
    [SerializeField] private bool isFood;
    [SerializeField] private bool isFoodBox;
    [SerializeField] private bool isCounter;
    [SerializeField] private bool isCounterInteractable;
    [SerializeField] private bool foodCatched = false;
    [SerializeField] private bool foodInside = false;
    [SerializeField] private bool foodOnCounter;

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
    private RaycastHit hit;
    private int maxDistance;
    private LayerMask layerMask;
    private float moveSpeed;

    #endregion

    private void Start()
    {
        anim = GetComponentInChildren<PlayerAnimation>();
        coll = GetComponent<PlayerCollision>();
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 direction = new Vector3(inputVector.x, 0f, inputVector.y);
        boxDetect = Physics.Raycast(transform.position, direction, out hit, maxDistance, layerMask);
    }

    private void Update()
    {
        // counter = coll._counter;
        // foodGo = coll._foodGo;
        // isFood = coll._isFood;
        // isCounter = coll._isCounter;
        // isCounterInteractable = coll._isCounterInteractable;
        // isFoodBox = coll._isFoodBox;

        // DebugInText("isFood: " + isFood + "\nisFoodBox: " + isFoodBox + "\nfoodCatched: " + foodCatched + "\nisCounter: " + isCounter + "\nisCounterInteractable: " + isCounterInteractable);

        Movement();
        HandleInteractions();
    }
    private void Movement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        isWalking = moveDir != Vector3.zero;

        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void DebugInText(string text)
    {
        debug.GetComponent<Text>().text = text;
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
        knifeGo.SetActive(false);
    }

    public void CutFood()
    {
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
}
