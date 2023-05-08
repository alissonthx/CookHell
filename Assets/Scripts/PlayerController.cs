using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region Variables
    private PlayerAnimation anim;
    private PlayerCollision coll;
    private CharacterController controller;
    private InputActions playerControlls;
    private InputActionReference Catch;

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

    [Header("Food")]
    [SerializeField]
    private bool isFood;
    [SerializeField]
    private bool foodCatched = false;
    [SerializeField]
    private GameObject foodGo;
    [SerializeField]
    private GameObject foodPoint;

    #endregion

    private void Awake()
    {
        playerControlls = new InputActions();
        playerControlls.Player.Move.performed += OnMove;
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<PlayerAnimation>();
        coll = GetComponent<PlayerCollision>();
    }

    private void Update()
    {
        isFood = coll._isFood;
        Move();
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

        foodCatched = false;
        foodGo.transform.SetParent(null);
        foodGo.transform.position = transform.position + transform.forward * 2f;
        foodGo.GetComponent<Rigidbody>().isKinematic = false;
    }

    private void Move()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 rawMovement = playerControlls.Player.Move.ReadValue<Vector2>();

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
    public void OnMove(InputAction.CallbackContext context)
    {
        
    }

    public void OnGetFood(InputAction.CallbackContext context)
    {
        Debug.Log("GetFood");
    }

    public void OnCatch(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Catch started");
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

    // public void CatchTouchFood(RaycastHit hit)
    // {
    //     if (hit.transform.gameObject.tag == "Food")
    //     {
    //         Debug.Log("Food touched");
    //         foodGo = hit.transform.gameObject;
    //         isFood = true;
    //     }
    //     else
    //     {
    //         isFood = false;
    //     }
    // }
}
