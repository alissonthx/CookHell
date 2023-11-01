using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterPoint;
    private KitchenObject kitchenObject;
    [SerializeField] private bool testing;
    [SerializeField] private Counter secondCounter;

    private void Update()
    {
        if (testing && Input.GetKeyDown(KeyCode.T))
        {
            if (kitchenObject != null)
            {
                kitchenObject.SetCounter(secondCounter);
            }
        }
    }

    // interact to instantiate food in blocks, need to change the object parent to player parent
    public void Interact()
    {
        if (kitchenObject == null)
        {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterPoint);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetCounter(this);
        }
        else
        {
            Debug.Log(kitchenObject.GetCounter());
        }
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

    public void SetKitchenObject()
    {
        throw new NotImplementedException();
    }
}
