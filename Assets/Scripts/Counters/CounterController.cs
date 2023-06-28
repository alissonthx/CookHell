using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterController : MonoBehaviour
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
}
