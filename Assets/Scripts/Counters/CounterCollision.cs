using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CounterCollision : MonoBehaviour
{
    #region Variables 
    [SerializeField]
    private bool boxDetect = false;
    [SerializeField]
    private bool isPlayer = false;
    public bool _isPlayer => this.isPlayer;

    [Space]

    [Header("Box Cast")]
    [SerializeField]
    private Vector3 boxSize;
    [SerializeField]
    private float maxDistance;
    [SerializeField]
    private LayerMask layerMask;
    private Vector3 origin;
    private Vector3 direction;
    private RaycastHit hit;

    #endregion
    
    private void Update()
    {
        origin = transform.position;
        direction = transform.forward;
        boxDetect = Physics.BoxCast(origin, boxSize, direction, out hit, transform.rotation, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
        // boxDetect = Physics.Raycast(origin, direction, out hit, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);

        if (boxDetect)
        {
            Debug.Log("ray touched something");
            if (hit.transform.CompareTag("Player"))
            {
                Debug.Log("ray touched player");
                isPlayer = true;
            }
            else
            {
                isPlayer = false;
            }
        }
        else
        {
            isPlayer = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(origin + direction * maxDistance, boxSize);
        // Gizmos.DrawRay(origin, direction * maxDistance);
    }
}