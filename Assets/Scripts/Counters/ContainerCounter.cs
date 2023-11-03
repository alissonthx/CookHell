using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabObject;
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    // interact to instantiate food in blocks, need to change the object parent to player parent
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (!player.HasKitchenObject())
            {
                // instantiate prefab object and set object parent to player
                Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
                kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);

                OnPlayerGrabObject?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
