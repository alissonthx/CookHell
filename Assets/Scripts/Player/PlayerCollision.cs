using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting.InputSystem;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    // #region Variables    
    // private PlayerAnimation anim;
    // [SerializeField]
    // private GameInput gameInput;

    // [Space]

    // [HideInInspector]
    // public RaycastHit hit;

    // [Space]

    // [Header("Player Collision")]
    // [SerializeField]
    // private float force = 10f;
    // [SerializeField]
    // private LayerMask layerMask;
    // [SerializeField]
    // private Vector3 boxSize = new Vector3(2f, 2f, 2f);
    // [SerializeField]
    // private float maxDistance = 2f;
    // [SerializeField]
    // private bool boxDetect;

    // public Vector3 origin;
    // public Vector3 direction;

    // [Space]

    // [Header("Food")]
    // private GameObject foodGo;
    // public GameObject _foodGo => this.foodGo;
    // private GameObject dishGo;
    // public GameObject _dishGo => this.dishGo;
    // public bool isFood = false;
    // public bool _isFood => this.isFood;

    // private GameObject counter;
    // public GameObject _counter => this.counter;
    // private GameObject foodBox;
    // public GameObject _foodBox => this.foodBox;

    // [Header("Bools")]
    // private bool isCounter = false;
    // public bool _isCounter => this.isCounter;
    // private bool isCounterInteractable = false;
    // public bool _isCounterInteractable => this.isCounterInteractable;
    // public bool _isFoodBox => this.isFoodBox;
    // public bool isFoodBox = false;
    // private bool isDishBox = false;

    // #endregion
    // private void Start()
    // {
    //     anim = GetComponentInChildren<PlayerAnimation>();
    // }

    

        // boxDetect = Physics.BoxCast(origin, boxSize, direction, out hit, transform.rotation, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);

        // if (boxDetect)
        // {
        //     switch (hit.transform.gameObject.tag)
        //     {
        //         case "InteractableBlocks":
        //             CounterBase();
        //             isCounterInteractable = true;
        //             break;
        //         case "NormalBlocks":
        //             CounterBase();
        //             isCounter = true;
        //             break;
        //         case "FoodBox":
        //             CounterBase();
        //             isFoodBox = true;
        //             break;
        //         case "DishBox":
        //             CounterBase();
        //             isDishBox = true;
        //             break;
        //         case "Food":
        //             Debug.Log("Food");
        //             FoodBase();
        //             break;
        //     }
        // }
        // else
        // {
        //     ResetInteract();
        // }
//     }

//     private void ResetInteract()
//     {
//         foodGo = null;
//         isFood = false;
//         isFoodBox = false;
//         isDishBox = false;
//         isCounter = false;
//         isCounterInteractable = false;

//         if (counter != null)
//         {
//             counter.transform.Find("Selected").gameObject.SetActive(false);
//         }
//     }

//     private void CounterBase()
//     {
//         counter = hit.transform.gameObject;
//         counter.transform.Find("Selected").gameObject.SetActive(true);
//     }

//     private void FoodBase()
//     {
//         foodGo = hit.transform.gameObject;
//         isFood = true;
//     }

//     private void DishBase()
//     {
//         dishGo = hit.transform.gameObject;
//         isFood = true;
//     }

//     // gravity is applied separately from the character controller
//     private void OnControllerColliderHit(ControllerColliderHit hit)
//     {
//         // Verify if the player collided with foods
//         if (hit.gameObject.CompareTag("Food"))
//         {
//             Rigidbody foodRigidbody = hit.gameObject.GetComponent<Rigidbody>();

//             foodRigidbody.AddForce(hit.moveDirection * force);

//             foodGo = hit.transform.gameObject;
//             isFood = true;
//         }
//     }
}