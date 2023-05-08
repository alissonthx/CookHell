using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FoodBoxController : MonoBehaviour
{
    #region Variables
    private FoodBoxAnimation anim;

    #endregion
    
    private void Start()
    {
        anim = GetComponentInChildren<FoodBoxAnimation>();        
    }

    // private void OnCollisionEnter(Collision other)
    // {
    //     if (other.gameObject.tag == "Player")
    //     {
    //         Debug.Log("Player in front of boxfood");
    //         Invoke("Instance", 0.5f);
    //     }
    // }
}