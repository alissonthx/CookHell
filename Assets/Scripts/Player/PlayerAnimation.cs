using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private GameObject knife;

    private string GRAB = "Grab";
    private string RELEASE = "Release";
    private string IS_WALKING = "isWalking";
    private string CUT = "Cut";

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        Player.Instance.OnPickedSomething += Player_OnPickedSomething;
        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
        DeliveryCounter.OnAnyObjectDelivered += DeliveryCounter_OnAnyObjectDelivered;
    }

    private void Player_OnPickedSomething(object sender, EventArgs e)
    {
        anim.SetTrigger(GRAB);
    }

    private void Update()
    {
        if (Player.Instance.IsWalking())
        {
            anim.SetBool(IS_WALKING, true);
        }
        else
        {
            anim.SetBool(IS_WALKING, false);
        }
    }

    private void BaseCounter_OnAnyObjectPlacedHere(object sender, EventArgs e)
    {
        anim.SetTrigger(RELEASE);
    }

    private void DeliveryCounter_OnAnyObjectDelivered(object sender, EventArgs e)
    {
        anim.SetTrigger(RELEASE);
    }


    private void CuttingCounter_OnAnyCut(object sender, EventArgs e)
    {
        if (!Player.Instance.HasKitchenObject())
        {
            anim.SetTrigger(CUT);
            StartCoroutine(KnifeShow(2.5f));
        }
    }

    private IEnumerator KnifeShow(float time)
    {
        knife.SetActive(true);
        yield return new WaitForSeconds(time);
        knife.SetActive(false);
    }

    private void OnDisable()
    {
        Player.Instance.OnPickedSomething -= Player_OnPickedSomething;
        BaseCounter.OnAnyObjectPlacedHere -= BaseCounter_OnAnyObjectPlacedHere;
        DeliveryCounter.OnAnyObjectDelivered -= DeliveryCounter_OnAnyObjectDelivered;
    }
}
