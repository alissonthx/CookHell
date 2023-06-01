using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodStateManager : MonoBehaviour
{
    public FoodBaseState currentState;
    public FoodInsideState InsideState = new FoodInsideState();
    public FoodHeldState HeldState = new FoodHeldState();
    public FoodThrowState ThrowState = new FoodThrowState();
    public FoodInstanceState InstanceState = new FoodInstanceState();

    private void Start(){
      currentState = InsideState;
      currentState.EnterState(this);  
    }

    private void Update(){
        currentState.UpdateState(this);        
    }

    private void OnCollisionEnter(Collision collision){
        currentState.OnCollisionEnter(this, collision);
    }

    public void SwitchState(FoodBaseState state){
        currentState = state;
        currentState.EnterState(this);
    }
}
