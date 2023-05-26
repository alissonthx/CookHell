using UnityEngine;

public abstract class FoodBaseState
{
    public abstract void EnterState(FoodStateManager food);
    public abstract void UpdateState(FoodStateManager food);
    public abstract void OnEnter(FoodStateManager food);
    public abstract void OnCollisionEnter(FoodStateManager food, Collision collision);
}
