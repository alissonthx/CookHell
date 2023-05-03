using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBoxController : MonoBehaviour
{
    public GameObject foodGo;
    [SerializeField]
    private GameObject foodPrefab;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player collided with food box");
            foodGo = Instantiate(foodPrefab, transform.position, Quaternion.identity);
        }
    }

}
