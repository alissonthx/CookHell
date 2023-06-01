using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CounterCollision : MonoBehaviour
{
    #region Variables 
    [SerializeField]   
    private bool isPlayer = false;
    public bool _isPlayer => this.isPlayer;

    [Space]

    [Header("Box Cast")]
    [SerializeField]
    private Vector3 boxSize = new Vector3(2f, 8f, 2f);
    [SerializeField]
    private float maxDistance = 2f;
    [SerializeField]
    private LayerMask layerMask;    
    private Vector3 origin;    
    private Vector3 direction;
    private RaycastHit hit;

    #endregion

    private void FixedUpdate()
    {
        origin = transform.position;
        direction = transform.forward;
        bool boxDetect = Physics.BoxCast(origin, boxSize, direction, out hit, transform.rotation, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);

        if (boxDetect)
        {
            if (hit.collider.CompareTag("Player"))
            {
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
        // Draw spherecast ray arounds the player
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(origin + direction * maxDistance, boxSize);
    }
}