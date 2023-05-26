using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private bool boxDetect;
    private RaycastHit hit;

    #endregion

    private void Start()
    {
        // dimension = new Vector3(2f, 2f, 2f);
        // coll = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Food")
        {
            Debug.Log("Food");
        }
        else if (other.tag == "Dish")
        {
            Debug.Log("Dish");
        }
        else if (other.tag == "Player")
        {
            Debug.Log("Player");
        }
        // switch (other.transform.gameObject.tag)
        // {
        //     case "Food":
        //         Debug.Log("Food");
        //         break;
        //     case "Dish":
        //         Debug.Log("Dish");
        //         break;
        //     case "Player":
        //         Debug.Log("Player");
        //         break;
        // }
    }

    // origin = transform.position;
    // direction = transform.forward;
    // boxDetect = Physics.BoxCast(origin, dimension, direction, out RaycastHit hit, transform.rotation, maxDistance, counterLayerMask, QueryTriggerInteraction.UseGlobal);

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(origin + direction * maxDistance, dimension);
    }

}