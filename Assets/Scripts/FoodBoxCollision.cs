using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBoxCollision : MonoBehaviour
{
    public bool isPlayer = false;
    public bool _isPlayer => this.isPlayer;

    [Space]

    [Header("Box Cast")]
    public Vector3 origin;
    public Vector3 direction;
    public Vector3 boxSize = new Vector3(2f, 8f, 2f);
    public float maxDistance = 2f;
    public LayerMask layerMask;
    public RaycastHit hit;

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
