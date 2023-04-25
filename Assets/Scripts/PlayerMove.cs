using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField]
    private float gravityValue = -9.81f;
    [SerializeField]
    private float playerSpeed = 20f;

    private CharacterController controller;
    private Vector3 playerVelocity;  


    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
        controller.Move(playerVelocity * Time.deltaTime);

    }
}
