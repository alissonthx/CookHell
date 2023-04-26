using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField]
    private float playerVelocity;

    [SerializeField]
    private Vector3 gravity = new Vector3(0, -9.81f, 0);

    private CharacterController controller;


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

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
        controller.Move(gravity * Time.deltaTime);
        controller.Move(move * playerVelocity * Time.deltaTime);

    }
}
