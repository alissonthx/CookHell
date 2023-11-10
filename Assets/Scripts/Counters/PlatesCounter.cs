using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO plateKitchenObject;
    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 4f;

    private void Update(){
        spawnPlateTimer += Time.deltaTime;
        if(spawnPlateTimer > spawnPlateTimerMax){
            KitchenObject.SpawnKitchenObject(plateKitchenObject, this);
        }
    }
}
