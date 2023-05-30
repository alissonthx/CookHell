using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CounterCollision : MonoBehaviour
{
    #region Variables    
    private Collider coll;

    [SerializeField]
    private LayerMask counterLayerMask;
    [SerializeField]
    private float maxDistance;
    [SerializeField]
    private Vector3 origin;
    [SerializeField]
    private Vector3 dimension;
    private Vector3 direction;
    [SerializeField]
    private bool boxDetect;
    private RaycastHit hit;

    private InputActions playerControlls;
    private InputActionReference GetingFood;

    #endregion

    private void Awake()
    {
        // playerControlls = new InputActions();
    }

    private void Start()
    {
        // dimension = new Vector3(2f, 2f, 2f);
        // coll = GetComponent<Collider>();
        // origin = transform.position;
        // direction = transform.forward;
    }

    private void FixedUpdate()
    {
        boxDetect = Physics.BoxCast(origin, dimension, direction, out RaycastHit hit, transform.rotation, maxDistance, counterLayerMask, QueryTriggerInteraction.UseGlobal);
        if (boxDetect)
        {
            if (hit.transform.CompareTag("Player"))
            {
                Debug.Log("Player detected");
            }
        }
    }

    public void OnTableFood(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Geting food");
        }
    }

    // private void OnEnable()
    // {
    //     playerControlls.Enable();
    // }

    // private void OnDisable()
    // {
    //     playerControlls.Disable();
    // }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(origin + direction * maxDistance, dimension);
    }

}