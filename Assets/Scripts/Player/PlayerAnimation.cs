using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private Player player;
    [SerializeField] private GameInput gameInput;
    private string GRAB = "Grab";
    private string RELEASE = "Release";
    private string IS_WALKING = "isWalking";

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        gameInput.OnInteractAction += OnInteractAction_OnPlayerGrabObject;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            anim.SetBool(IS_WALKING, true);
        }
        else
        {
            anim.SetBool(IS_WALKING, false);
        }
    }

    private void OnInteractAction_OnPlayerGrabObject(object sender, System.EventArgs e)
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
