using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterAnimation : MonoBehaviour
{
    private const string OPEN_CLOSE = "OpenClose";
    private Animator anim;    

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        ContainerCounter.OnPlayerGrabObject += ContainerCounter_OnPlayerGrabObject;
    }

    private void ContainerCounter_OnPlayerGrabObject(object sender, System.EventArgs e)
    {
        anim.SetTrigger(OPEN_CLOSE);
    }
}
