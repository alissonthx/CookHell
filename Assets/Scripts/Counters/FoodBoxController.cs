using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FoodBoxController : MonoBehaviour
{
    #region Variables
    private CounterCollision counterCollision;
    private CounterAnimation anim;

    #endregion

    private void Start()
    {
        anim = GetComponentInChildren<CounterAnimation>();
        counterCollision = GetComponent<CounterCollision>();
    }

    public void OnAnimation(InputAction.CallbackContext context)
    {
        if (context.performed && counterCollision._isPlayer)
            anim.SetTrigger("open");
    }
}