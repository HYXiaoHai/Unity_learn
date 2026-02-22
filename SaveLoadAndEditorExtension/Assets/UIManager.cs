using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public TMP_Text shootText;
    public TMP_Text scoreText;
    public TMP_Text messageText;

    public int shootNume = 0;
    public int score = 0;

    public Toggle musicToggle;
    public AudioSource musicAdiu;
    private bool musicOn = true;
    private void Awake()
    {
        Instance = this;
        if(PlayerPrefs.HasKey("MusicOn"))
        {
            if(PlayerPrefs.GetInt("MusicOn") == 1)
            {
                musicToggle.isOn = true;
                musicAdiu.enabled = true;
            }
            else
            {
                musicToggle.isOn = false;
                musicAdiu.enabled = false;
            }
        }
        else
        {
            musicToggle.isOn = true;
            musicAdiu.enabled = true;
        }
    }
    public void AddShoot()
    {
        shootNume += 1;
    }

    public void AddScroe()
    {
        score += 1;
    }

    // Update is called once per frame
    private void Update()
    {
        shootText.text = "Éä»÷Êý:" + shootNume.ToString();
        scoreText.text = "µÃ·Ö:" + score.ToString();
    }
    public void MusicSwitch()
    {
        if(musicToggle.isOn == false)
        {
            musicOn = false;
            musicAdiu.enabled = false;
            PlayerPrefs.SetInt("MusicOn", 0);
        }
        else
        {
            musicOn = true;
            musicAdiu.enabled = true;
            PlayerPrefs.SetInt("MusicOn", 1);
        }
        PlayerPrefs.Save();
    }
    public void ShowMessage(string str)
    {
        messageText.text = str;
    }

}
