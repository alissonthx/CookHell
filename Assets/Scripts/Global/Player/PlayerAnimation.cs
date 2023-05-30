using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [HideInInspector]
    public Animator anim; 
    private PlayerCollision col;

    void Start()
    {
        anim = GetComponent<Animator>();        
    }

    void Update()
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

    public void SetBool(string name, bool value){
        anim.SetBool(name, value);
    }
}
