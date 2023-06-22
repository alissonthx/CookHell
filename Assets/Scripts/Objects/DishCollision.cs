using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishCollision : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private float maxDistance = 4f;
    private float distance = 5f;
    private bool rayDetect;

    private void Update()
    {
        RayDetection();
    }

    private void RayDetection()
    {
        RaycastHit hit;

        Ray fowardRay = new Ray(transform.position, -Vector3.forward);
        Ray upRay = new Ray(transform.position, -Vector3.up);
        Debug.DrawRay(transform.position, Vector3.back * distance, Color.red);
        Debug.DrawRay(transform.position, Vector3.down * distance, Color.red);

        if (Physics.Raycast(fowardRay, out hit))
        {
            if (hit.collider.tag == "Player" && hit.distance < maxDistance)
            {
                // Debug.Log("Player is in range");
            }
        }

        if (Physics.Raycast(upRay, out hit))
        {
            if (hit.collider.tag == "InteractableBlocks" || hit.collider.tag == "NormalBlocks")
            {
                // Debug.Log("Dish is on counter");
            }
        }
    }
}
