using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerAnimation anim;

    [Header("Player Stats")]
    [SerializeField]
    private float playerVelocity;
    [SerializeField]
    private Vector3 gravity = new Vector3(0, -9.81f, 0);

    [Header("Food")]
    [SerializeField]
    private bool isFood = false;
    [SerializeField]
    private bool foodCatched = false;
    [SerializeField]
    private GameObject foodGo;
    [SerializeField]
    private GameObject foodPoint;

    private CharacterController controller;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<PlayerAnimation>();
    }

    private void Update()
    {
        Move();

        if (Input.GetButtonDown("Fire2") && isFood && !foodCatched)
        {
            anim.SetBool("isCatching", true);

            foodGo.transform.SetParent(foodPoint.transform);
            foodGo.transform.position = foodPoint.transform.position;
            foodGo.GetComponent<Rigidbody>().isKinematic = true;
            foodCatched = true;
        }
        else if (Input.GetButtonDown("Fire2") && foodCatched)
        {
            anim.SetBool("isCatching", false);

            foodCatched = false;
            foodGo.transform.SetParent(null);
            foodGo.transform.position = transform.position + transform.forward * 2f;
            foodGo.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    public void Catch(RaycastHit hit)
    {
        if (hit.transform.gameObject.tag == "Food")
        {
            Debug.Log("Food touched");
            foodGo = hit.transform.gameObject;
            isFood = true;
        }
        else
        {
            isFood = false;
        }
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
