using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodThrowState : FoodBaseState
{
    public override void EnterState(FoodStateManager food)
    {
        
    }

    public override void UpdateState(FoodStateManager food)
    {

    }

    public override void OnEnter(FoodStateManager food)
    {

    }

    public override void OnCollisionEnter(FoodStateManager food, Collision collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("Player"))
        {
            Debug.Log("ThrowState: OnCollisionEnter: Tag: Player");
            food.SwitchState(food.HeldState);
            other.GetComponent<PlayerAnimation>().SetBool("isCatching", false);

            other.transform.SetParent(null);
            other.transform.position = other.transform.position + other.transform.forward * 2f;
            other.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
