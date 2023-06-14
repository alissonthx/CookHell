using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterAnimation : MonoBehaviour
{
    [HideInInspector]
    public Animator anim;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    public void SetTrigger(string triggerName)
    {
        anim.SetTrigger(triggerName);
    }
}
