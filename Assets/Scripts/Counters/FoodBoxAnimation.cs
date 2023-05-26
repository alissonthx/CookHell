using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBoxAnimation : MonoBehaviour
{
    [HideInInspector]
    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void SetTrigger(string triggerName)
    {
        anim.SetTrigger(triggerName);
    }
}
