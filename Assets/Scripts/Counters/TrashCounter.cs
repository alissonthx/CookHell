using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    new public static void ResetStaticData()
    {
        OnAnyObjectTrashed = null;
    }

    public static event EventHandler OnAnyObjectTrashed;
    public override void Interact(Player player)
    {
        // Object trashed if player is hold something
        if (player.HasKitchenObject())
        {
            player.GetKitchenObject().DestroySelf();

            OnAnyObjectTrashed?.Invoke(this, EventArgs.Empty);
        }
    }
}
