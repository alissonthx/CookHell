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

    private void Update()
    {
        timerImage.fillAmount = KitchenGameManager.Instance.GamePlayingTimerNormalize();

        updateTimer(KitchenGameManager.Instance.GetGamePlayingTimer());
    }

    private void updateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        textTimer.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
