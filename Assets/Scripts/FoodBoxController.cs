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

    public void OnAnimation(InputAction.CallbackContext context)
    {
        if (context.performed)
            anim.SetTrigger("open");            
    }
}