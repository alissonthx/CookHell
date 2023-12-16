using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AmbienceManager : MonoBehaviour
{
    public static AmbienceManager Instance { get; private set; }

    private AudioSource audioSource;
    private float volume;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public float GetVolume()
    {
        return volume;
    }

    public void ChangeVolume()
    {
        volume += .1f;
        if (volume > 1f)
        {
            volume = 0f;
        }
        audioSource.volume = volume;
    }
}
