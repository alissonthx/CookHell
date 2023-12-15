using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMenuAnimation : MonoBehaviour
{
    private float timeToChangeAnimationMax = 16f;
    private float timeToChangeAnimation;
    private Animator anim;
    private string IDLE_VARIANT = "idleVariant";

    private void Awake()
    {
        timeToChangeAnimation = timeToChangeAnimationMax;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (timeToChangeAnimation <= 0)
        {
            timeToChangeAnimation = timeToChangeAnimationMax;
            // change animation to idle variant
            anim.SetTrigger(IDLE_VARIANT);            
        }

        if (timeToChangeAnimation > 0)
        {
            timeToChangeAnimation -= Time.deltaTime;
        }
    }
}
