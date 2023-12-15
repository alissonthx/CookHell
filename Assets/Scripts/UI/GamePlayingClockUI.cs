using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayingClockUI : MonoBehaviour
{
    [SerializeField] private Image timerImage;
    [SerializeField] private TextMeshProUGUI textTimer;

    // private void Start()
    // {
    //     KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;
    // }

    private void Update()
    {
        timerImage.fillAmount = KitchenGameManager.Instance.GamePlayingTimerNormalize();

        textTimer.text = KitchenGameManager.Instance.GetGamePlayingTimer().ToString();
    }

    // private float FormatTimer(float timer){

    // }  

    // private void KitchenGameManager_OnStateChanged(object sender, EventArgs e)
    // {
    //     if (KitchenGameManager.Instance.IsCountdownToStartActive())
    //     {

    //     }
    //     else
    //     {
    //     }
    // }
}
