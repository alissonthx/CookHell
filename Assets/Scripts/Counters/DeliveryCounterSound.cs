using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounterSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        // DeliveryCounter.Instance.OnAnyObjectPlacedHere += DeliveryCounter_OnAnyObjectPlacedHere;
    }

    private void DeliveryCounter_OnAnyObjectPlacedHere(object sender, EventArgs e)
    {
        audioSource.Play();
    }

}
