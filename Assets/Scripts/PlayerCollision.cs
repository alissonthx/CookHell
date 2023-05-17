using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    #region Variables
    private PlayerController controller;
    private PlayerAnimation anim;
    private PlayerController playerControl;
    private FoodBoxController foodBoxController;

    [Space]

    [HideInInspector]
    public GameObject foodGo;
    [HideInInspector]
    public GameObject foodBox;
    [HideInInspector]
    public Collider coll;
    [HideInInspector]
    public RaycastHit hit;

    [Space]

    [Header("Player Collision")]
    public float force = 10f;
    public LayerMask layerMask;
    public float sphereRadius;
    // public float maxDistance = 4f;
    public Collider[] sphereDetect;

    public Vector3 origin;
    public Vector3 direction;

    [Space]

    [Header("Food")]
    [HideInInspector]
    public bool isFood = false;
    public bool _isFood => this.isFood;
    [HideInInspector]
    public bool isFoodBox = false;
    public bool _isFoodBox => this.isFoodBox;

    private Vector3 distanceToFood;

    #endregion
    private void Start()
    {
        controller = GetComponent<PlayerController>();
        anim = GetComponentInChildren<PlayerAnimation>();
        coll = controller.GetComponent<Collider>();
        playerControl = GetComponent<PlayerController>();
    }

    private void FixedUpdate()
    {
        origin = transform.position;
        direction = transform.forward;
        // sphereDetect = Physics.SphereCast(origin, sphereRadius, direction, out hit, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
        sphereDetect = Physics.OverlapSphere(origin, sphereRadius, layerMask);

        if (sphereDetect.Length > 0)
        {
            // switch (hit.transform.gameObject.tag)
            GameObject sphereGo = sphereDetect[0].gameObject;
            switch (sphereGo.tag)
            {
                case "InteractableBlocks":
                    // Debug.Log("InteractableBlocks around!");
                    ResetInteract();
                    break;
                case "NormalBlocks":
                    ResetInteract();
                    // Debug.Log("NormalBlocks around!");
                    break;
                case "FoodBox":
                    // Debug.Log("FoodBox around!");
                    // foodBox = hit.transform.gameObject;
                    foodBox = sphereGo;
                    sphereGo.transform.Find("Selected").gameObject.SetActive(true);
                    isFoodBox = true;
                    break;
                case "Food":                    
                    // Debug.Log("Food around!");                    
                    // foodGo = hit.transform.gameObject;
                    foodGo = sphereGo.transform.gameObject;
                    isFood = true;                    
                    break;
            }
        }
        else
        {
            ResetInteract();
        }
    }
    private void ResetInteract()
    {
        if (foodBox != null)
        {
            foodBox.transform.Find("Selected").gameObject.SetActive(false);
        }
        isFood = false;
        isFoodBox = false;
    }

    // gravity is applied separately from the character controller
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