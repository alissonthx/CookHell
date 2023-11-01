using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO; 

   
    // interact to instantiate food in blocks, need to change the object parent to player parent
    public override void Interact(Player player)
    {
    }    
}
