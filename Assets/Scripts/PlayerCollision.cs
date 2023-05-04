using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private PlayerController controller;
    private PlayerAnimation anim;
    private PlayerController playerControl;

    
    [HideInInspector]
    public Collider col;
    public RaycastHit hit;


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
    public GameObject[] foods;
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
                    Debug.Log("FoodBox is colliding");
                    break;
                case "Food":
                    Debug.Log("Food is colliding");
                    playerControl.Catch(hit);
                    break;
            }
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