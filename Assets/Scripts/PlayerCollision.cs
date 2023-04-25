using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [HideInInspector]
    public RaycastHit hit;
    [HideInInspector]
    public Collider col;

    [Header("Player Collision")]
    public GameObject[] foods;
    public float distanceToFood;
    public LayerMask layerMask;
    public int highlightMask;
    public float sphereRadius;
    public float maxDistance;
    public bool hitDetect;
    public bool sphereDetect;
    public Vector3 origin;
    public Vector3 direction;


    private void Start()
    {
        foods = GameObject.FindGameObjectsWithTag("Food");
        col = GetComponent<Collider>();
    }

    private void Awake()
    {
        highlightMask = LayerMask.NameToLayer("Highlight");
    }

    private void FixedUpdate()
    {
        hitDetect = Physics.BoxCast(col.bounds.center, transform.localScale, transform.forward, out hit, transform.rotation, maxDistance);

        if (hitDetect)
        {
            if (hit.transform.CompareTag("InteractableBlocks"))
            {

            }
            else if (hit.transform.CompareTag("NormalBlocks"))
            {

            }
        }

        // Sphere to detects if player near to food objects
        origin = transform.position;
        direction = transform.forward;

        sphereDetect = Physics.SphereCast(origin, sphereRadius, direction, out hit, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);

        if (sphereDetect)
        {
            distanceToFood = hit.distance;
            if (hit.transform.CompareTag("Food"))
            {
                Debug.Log("Food around!");
            }
        }
    }

    //Draw the BoxCast as a gizmo to show where it currently is testing
    private void OnDrawGizmos()
    {
        // Draw spherecast ray arounds the player
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanceToFood);

        Gizmos.color = Color.red;

        //Check if there has been a hit yet
        if (hitDetect)
        {
            //Draw a Ray forward from GameObject toward the hit
            Gizmos.DrawRay(transform.position, transform.forward * hit.distance);
            //Draw a cube that extends to where the hit exists
            Gizmos.DrawWireCube(transform.position + transform.forward * hit.distance, transform.localScale);
        }
        //If there hasn't been a hit yet, draw the ray at the maximum distance
        else
        {
            //Draw a Ray forward from GameObject toward the maximum distance
            Gizmos.DrawRay(transform.position, transform.forward * maxDistance);
            //Draw a cube at the maximum distance
            Gizmos.DrawWireCube(transform.position + transform.forward * maxDistance, transform.localScale);
        }
    }

}
