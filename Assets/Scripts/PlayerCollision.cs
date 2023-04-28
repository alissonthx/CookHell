using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private PlayerController controller;
    private PlayerAnimation anim;

    [HideInInspector]
    public RaycastHit hit;
    [HideInInspector]
    public Collider col;

    private bool isFood = false;

    [Header("Player Collision")]
    public float force = 10f;
    public LayerMask layerMask;
    public int highlightMask;
    public float sphereRadius;
    public float maxDistance = 4f;
    public bool hitDetect;
    public bool sphereDetect;



    public Vector3 origin;
    public Vector3 direction;

    [Header("Food")]
    public GameObject[] foods;
    public GameObject foodGo;
    [SerializeField]
    private GameObject foodPoint;

    private void Start()
    {
        foods = GameObject.FindGameObjectsWithTag("Food");
        controller = GetComponent<PlayerController>();
        anim = GetComponentInChildren<PlayerAnimation>();
        col = controller.GetComponent<Collider>();
    }

    private void Awake()
    {
        highlightMask = LayerMask.NameToLayer("Highlight");
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire2") && isFood)
        {
            anim.SetBool("isCatching", true);

            foodGo.transform.SetParent(foodPoint.transform);
            foodGo.transform.position = foodPoint.transform.position;
            foodGo.GetComponent<Rigidbody>().isKinematic = true;

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
                    FoodCatch();
                    break;
            }

        }
    }

    private void FoodCatch()
    {
        if (hit.transform != null)
        {
            foodGo = hit.transform.gameObject;
            isFood = true;
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Verify if the player collided with foods
        if (hit.gameObject.CompareTag("Food"))
        {
            Rigidbody foodRigidbody = hit.gameObject.GetComponent<Rigidbody>();

            foodRigidbody.AddForce(hit.moveDirection * force);
        }
    }

    private void OnDrawGizmos()
    {
        // Draw spherecast ray arounds the player
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sphereRadius);
    }

}
