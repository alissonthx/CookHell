using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Player player;
    private Animator anim;
    [SerializeField] private GameObject knife;
    [SerializeField] private GameInput gameInput;

    private string GRAB = "Grab";
    private string RELEASE = "Release";
    private string IS_WALKING = "isWalking";
    private string CUT = "Cut";

    private void Awake()
    {
        anim = GetComponent<Animator>();
        player = GetComponentInParent<Player>();
    }

    private void Start()
    {
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        ContainerCounter.OnPlayerGrabObject += ContainerCounter_OnPlayerGrabObject;
    }

    private void ContainerCounter_OnPlayerGrabObject(object sender, EventArgs e)
    {
        if (!player.HasKitchenObject())
        {
            anim.SetTrigger(GRAB);
        }
        else
        {
            anim.SetTrigger(RELEASE);
        }
    }

    private void CuttingCounter_OnAnyCut(object sender, EventArgs e)
    {
        if (!player.HasKitchenObject())
        {
            anim.SetTrigger(CUT);
            StartCoroutine(KnifeShow(2.5f));
        }
    }

    private void Update()
    {
        if (player.IsWalking())
        {
            anim.SetBool(IS_WALKING, true);
        }
        else
        {
            anim.SetBool(IS_WALKING, false);
        }
    }

    private IEnumerator KnifeShow(float time)
    {
        knife.SetActive(true);
        yield return new WaitForSeconds(time);
        knife.SetActive(false);
    }
}
