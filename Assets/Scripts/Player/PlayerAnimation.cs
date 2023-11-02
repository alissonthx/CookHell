using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private GameInput gameInput;
    private string GRAB = "Grab";

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
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
    }

    private void OnInteractAction_OnPlayerGrabObject(object sender, System.EventArgs e)
    {
        anim.SetTrigger(GRAB);
    }
}
