using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuPanel : MonoBehaviour
{
    public Button startButton;
    public Button settingButton;
    public Button progressButton;
    public Button exitButton;

    private bool isMainmemu = true;

    private void Awake()
    {
        startButton = GameObject.Find("StartButton").GetComponent<Button>();
        settingButton = GameObject.Find("SettingButton").GetComponent<Button>();
        progressButton = GameObject.Find("ProgressButton").GetComponent<Button>();
        exitButton = GameObject.Find("ExitButton").GetComponent<Button>();
    }

    void Start()
    {
        //lumeda±í´ïÊ½
        //startButton.onClick.AddListener(call:()=>
        //{
        //    SceneManager.LoadScene("02-LevelSelect");
        //}
        //);
        startButton.onClick.AddListener(StatButton);
        exitButton.onClick.AddListener(ExiteButton);
    }

    private void StatButton()
    {
        SceneManager.LoadScene("02-LevelSelect");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)&&isMainmemu)
        {
            ExiteButton();
        }
    }

    private void ExiteButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

}
