using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    new public static void ResetStaticData()
    {
        OnAnyGrab = null;
    }

    public static event EventHandler OnAnyGrab;
    public event EventHandler OnPlayerGrabObject;
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    // interact to instantiate food in blocks, need to change the object parent to player parent
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (!player.HasKitchenObject())
            {
                KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);

                OnPlayerGrabObject?.Invoke(this, EventArgs.Empty);
                
                OnAnyGrab?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
