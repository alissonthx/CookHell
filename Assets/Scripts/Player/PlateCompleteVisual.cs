using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    public struct KitchenObjectSO_GameObject{
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }
  [SerializeField] private PlateKitchenObject plateKitchenObject; 
  [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSOGameObjectsList; 

  private void Start(){
    plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
  }

  private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e){
    // e.kitchenObjectSO
  }
}
