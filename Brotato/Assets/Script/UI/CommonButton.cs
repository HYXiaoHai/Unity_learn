using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class CommonButton : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private Image image;
    private TextMeshProUGUI text;
    private Button button;
    private AudioSource audioSource;
    private AudioClip audioClip;
    private void Awake()
    {
           image = GetComponent<Image>();
           audioSource = GetComponent<AudioSource>();
           text = GetComponentInChildren<TextMeshProUGUI>();
        audioClip = Resources.Load < AudioClip > ("Music/≤Àµ•“Ù–ß"); 
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        audioSource.PlayOneShot(audioClip);
        image.color = new Color(255, 255, 255);
        text.color = Color.black;
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        text.color = new Color(255, 255, 255);
        image.color = Color.black;
    }
}
