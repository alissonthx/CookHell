using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    #region Variables
    public static Player Instance { get; private set; }
    private PlayerAnimation anim;
    [SerializeField]
    private GameInput gameInput;

    [Header("Debug")]
    [Space]
    [SerializeField]
    private GameObject debug;

    [Space]
    [Header("Stats")]
    private bool isWalking;
    [SerializeField]
    private float moveSpeed = 28.0f;
    [Header("Bools")]
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
    [SerializeField]
    private LayerMask countersLayerMask;
    private Vector3 lastInteractDir;
    private Counter selectedCounter;
    [HideInInspector]
    #endregion
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public Counter selectedCounter;
    }


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one Player instance");
        }
        Instance = this;
    }

    private void Start()
    {
        anim = GetComponentInChildren<PlayerAnimation>();
        gameInput.OnInteractAction += GameInput_OnInteractAction;       
    }

    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        float interactDistance = 4f;
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out Counter counter))
            {
                // has counter
                counter.Interact();
            }
        }
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        float interactDistance = 4f;
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out Counter counter))
            {
                // has counter                
                if (counter != selectedCounter)
                {
                    SetSelectedCounter(counter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
        Debug.Log(selectedCounter);
    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        float playerRadius = 2f;
        float playerHeight = 6f;
        float moveDistance = moveSpeed * Time.deltaTime;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);
        isWalking = moveDir != Vector3.zero;
        float rotateSpeed = 10f;

        if (!canMove)
        {
            // CapsuleCast to detects
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                // can move only on the X 
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    // can move only on the Z
                    moveDir = moveDirZ;
                }
                else
                {
                    // Cannot move any direction
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDir * moveSpeed * Time.deltaTime;
        }
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    private void SetSelectedCounter(Counter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
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
        // Debug.Log("CatchDish");
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
