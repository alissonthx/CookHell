using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBoxController : MonoBehaviour
{
    private FoodBoxAnimation anim;
    private GameObject foodGo;
    [SerializeField]
    private GameObject foodPrefab;

    private void Start()
    {
        anim = GetComponentInChildren<FoodBoxAnimation>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player in front of boxfood");
            Invoke("Instance", 0.5f);
        }
    }

    private void Instance()
    {
        Debug.Log("Player instantiate food");
        foodGo = Instantiate(foodPrefab, transform.position, Quaternion.identity);
        anim.SetTrigger("open");                
    }
}