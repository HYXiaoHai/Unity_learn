using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
//≤•∑≈“Ù∆µµƒ¿‡
//
public class No18_AudioSourse : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip musicClip;
    public AudioClip soudClip;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = musicClip;
    }

    void Update()
    {
        
    }
}
