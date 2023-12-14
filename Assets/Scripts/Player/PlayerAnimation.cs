using System;
using System.Collections;
using System.Collections.Generic;
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
    }

    private void Start()
    {
        gameInput.OnInteractAction += OnInteractAction_OnPlayerGrabObject;
        gameInput.OnInteractAlternateAction += OnInteractAlternateAction_OnCut;
    }

    private void OnInteractAlternateAction_OnCut(object sender, EventArgs e)
    {
        anim.SetTrigger(CUT);
        StartCoroutine(KnifeShow(2.5f));
    }

    private IEnumerator KnifeShow(float time)
    {
        knife.SetActive(true);
        yield return new WaitForSeconds(time);
        knife.SetActive(false);
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

    private void OnInteractAction_OnPlayerGrabObject(object sender, EventArgs e)
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
}
