using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private PlayerController controller;

    [HideInInspector]
    public RaycastHit hit;
    [HideInInspector]
    public Collider col;

    [Header("Player Collision")]
    public GameObject[] foods;
    public float force = 10f;    
    public LayerMask layerMask;
    public int highlightMask;
    public float sphereRadius;
    public float maxDistance = 4f;
    public bool hitDetect;
    public bool sphereDetect;
    public Vector3 origin;
    public Vector3 direction;


    private void Start()
    {
        foods = GameObject.FindGameObjectsWithTag("Food");
        controller = GetComponent<PlayerController>();
        col = controller.GetComponent<Collider>();
    }

    private void Awake()
    {
        highlightMask = LayerMask.NameToLayer("Highlight");
    }

    private void FixedUpdate()
    {
        // hitDetect = Physics.BoxCast(col.bounds.center, transform.localScale, transform.forward, out hit, transform.rotation, maxDistance);
        origin = transform.position;
        direction = transform.forward;
        sphereDetect = Physics.SphereCast(origin, sphereRadius, direction, out hit, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);

        if (sphereDetect)
        {
            switch (hit.transform.gameObject.tag)
            {
                case "InteractableBlocks":
                    Debug.Log("InteractableBlocks around!");
                    break;
                case "NormalBlocks":
                    Debug.Log("NormalBlocks around!");
                    break;
                case "Food":
                    Debug.Log("Food around!");
                    break;
                case "FoodBox":
                    Debug.Log("Food Box here");
                    break;
            }            
        }
    }


    // make physics for foods objects
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
