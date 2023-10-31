using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterPoint;
    public void Interact(){
        Debug.Log("interact");
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterPoint);
        kitchenObjectTransform.localPosition = Vector3.zero;

        kitchenObjectTransform.GetComponent<KitchenObject>().GetKitchenObjectSO();
    }
    
}
