using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private Transform counterPoint;
    private KitchenObject kitchenObject;

    public virtual void Interact(Player player){
        Debug.LogError("BaseCounter.Interact();");
    }
    public virtual void InteractAlternate(Player player){
        // Debug.LogError("BaseCounter.InteractAlternate();");
    }
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return counterPoint;
    }
}
