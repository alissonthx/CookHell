using UnityEngine;

public class FoodHeldState : FoodBaseState
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
            // Debug.Log("HeldState: OnCollisionEnter: Tag: Player");
            other.GetComponent<PlayerAnimation>().SetBool("isCatching", true);

            food.transform.SetParent(other.transform);
            food.transform.position = other.transform.position;
            food.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
