using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterAnimation : MonoBehaviour
{
    private const string OPEN_CLOSE = "OpenClose";
    private Animator anim;
    [SerializeField] private ContainerCounter countainerCounter;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        countainerCounter.OnPlayerGrabObject += ContainerCounter_OnPlayerGrabObject;
    }

    private void ContainerCounter_OnPlayerGrabObject(object sender, System.EventArgs e)
    {
        anim.SetTrigger(OPEN_CLOSE);
    }
}
