using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedVisual : MonoBehaviour
{
    public void SetSelected(bool state){
        if(this == null){
            return;
        }
        gameObject.SetActive(state);
    }

}
