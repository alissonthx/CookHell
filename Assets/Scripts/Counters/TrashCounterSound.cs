using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounterSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    }

    private void TrashCounter_OnAnyObjectTrashed(object sender, EventArgs e)
    {
        audioSource.Play();
    }
}
