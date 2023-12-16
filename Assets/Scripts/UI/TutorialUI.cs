using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI keyMoveUpText;   
    [SerializeField] private TextMeshProUGUI keyMoveDownText;   
    [SerializeField] private TextMeshProUGUI keyMoveLeftText;   
    [SerializeField] private TextMeshProUGUI keyMoveRightText;   
    [SerializeField] private TextMeshProUGUI keyInteractText;   
    [SerializeField] private TextMeshProUGUI keyInteractAlternateText;   
    [SerializeField] private TextMeshProUGUI keyPauseText;   
    [SerializeField] private TextMeshProUGUI keyGamepadInteractText;   
    [SerializeField] private TextMeshProUGUI keyGamepadInteractAlternateText;   
    [SerializeField] private TextMeshProUGUI keyGamepadPauseText;   

    private void Start(){
        UpdateVisual();
    }

    private void UpdateVisual(){
        // update all binding text to actual key         
        // needs to implement change keybinding!


        // keyMoveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
    }
}
