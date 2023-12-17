using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public static DeliveryCounter Instance { get; private set; }

    public event EventHandler OnAnyObjectDelivered;

    private void Awake()
    {
        Instance = this;
    }

    public override void Interact(Player player)
    {
        // if the player is holding something
        if (player.HasKitchenObject())
        {
            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                // Only accepts Plates
                player.GetKitchenObject().DestroySelf();

                DeliveryManager.Instance.DeliveryRecipe(plateKitchenObject);
                OnAnyObjectDelivered?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
