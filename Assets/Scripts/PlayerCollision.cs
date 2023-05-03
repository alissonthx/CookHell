using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private PlayerController controller;
    private PlayerAnimation anim;
    private PlayerController playerControl;

    [HideInInspector]
    public RaycastHit hit;
    [HideInInspector]
    public Collider col;


    [Header("Player Collision")]
    public float force = 10f;
    public LayerMask layerMask;
    public int highlightMask;
    public float sphereRadius;
    public float maxDistance = 4f;
    public bool sphereDetect;

    public Vector3 origin;
    public Vector3 direction;

    [Header("Food")]
    [SerializeField]
    private bool isFood = false;
    [SerializeField]
    private bool foodCatched = false;
    public GameObject[] foods;
    public GameObject foodGo;
    [SerializeField]
    private GameObject foodPoint;
    private Vector3 distanceToFood;

    private void Start()
    {
        foods = GameObject.FindGameObjectsWithTag("Food");
        controller = GetComponent<PlayerController>();
        anim = GetComponentInChildren<PlayerAnimation>();
        col = controller.GetComponent<Collider>();
        playerControl = GetComponent<PlayerController>();
    }

    private void Awake()
    {
        highlightMask = LayerMask.NameToLayer("Highlight");
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire2") && isFood && !foodCatched)
        {
            anim.SetBool("isCatching", true);

            foodGo.transform.SetParent(foodPoint.transform);
            foodGo.transform.position = foodPoint.transform.position;
            foodGo.GetComponent<Rigidbody>().isKinematic = true;
            foodCatched = true;
        }
        else if (Input.GetButtonDown("Fire2") && foodCatched)
        {
            anim.SetBool("isCatching", false);

            foodCatched = false;
            foodGo.transform.SetParent(null);
            foodGo.transform.position = transform.position + transform.forward * 2f;
            foodGo.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    public void Catch()
    {
        if (hit.transform.gameObject.tag == "Food")
        {
            foodGo = hit.transform.gameObject;
            isFood = true;
        }
        else
        {
            isFood = false;
        }
    }

    private void FixedUpdate()
    {
        origin = transform.position;
        direction = transform.forward;
        sphereDetect = Physics.SphereCast(origin, sphereRadius, direction, out hit, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);

        if (sphereDetect)
        {
            switch (hit.transform.gameObject.tag)
            {
                case "InteractableBlocks":
                    break;
                case "NormalBlocks":
                    break;
                case "FoodBox":
                    break;
                case "Food":
                    Catch();
                    break;
            }
        }
        else
        {
            isFood = false;
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Verify if the player collided with foods
        if (hit.gameObject.CompareTag("Food"))
        {
            isFood = true;
            Rigidbody foodRigidbody = hit.gameObject.GetComponent<Rigidbody>();

            foodRigidbody.AddForce(hit.moveDirection * force);
        }
        else
        {
            isFood = false;
        }
    }

    private void OnDrawGizmos()
    {
        // Draw spherecast ray arounds the player
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sphereRadius);
    }

}