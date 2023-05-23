using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FoodBoxController : MonoBehaviour
{
    #region Variables
    private FoodBoxCollision foodBoxCollision;
    private FoodBoxAnimation anim;

    #endregion

    private void Start()
    {
        anim = GetComponentInChildren<FoodBoxAnimation>();
        foodBoxCollision = GetComponent<FoodBoxCollision>();
    }

    public void OnAnimation(InputAction.CallbackContext context)
    {
        if (context.performed && foodBoxCollision._isPlayer)
            anim.SetTrigger("open");
    }
}