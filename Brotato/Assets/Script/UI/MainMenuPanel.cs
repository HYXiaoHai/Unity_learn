using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuPanel : MonoBehaviour
{
    public static MainMenuPanel instance;
    public CanvasGroup mainMenuPanel;
    public CanvasGroup confirmPanel;

    public Button startButton;
    public Button settingButton;
    public Button progressButton;
    public Button exitButton;

    public Button continueButton;
    public Button newGameButton;

    private bool isMainmemu = true;

    private void Awake()
    {
        instance = this;
        mainMenuPanel = GetComponent<CanvasGroup>();
        confirmPanel = GameObject.Find("ConfirmPanel").GetComponent<CanvasGroup>();

        startButton = GameObject.Find("StartButton").GetComponent<Button>();
        settingButton = GameObject.Find("SettingButton").GetComponent<Button>();
        progressButton = GameObject.Find("ProgressButton").GetComponent<Button>();
        exitButton = GameObject.Find("ExitButton").GetComponent<Button>();
        continueButton = GameObject.Find("ContinueButton").GetComponent<Button>();
        newGameButton = GameObject.Find("NewGameButton").GetComponent<Button>();
    }

    void Start()
    {
        startButton.onClick.AddListener(StartButton);
        progressButton.onClick.AddListener(ProgressButton);
        exitButton.onClick.AddListener(ExiteButton);

        // 为确认面板的按钮添加事件
        continueButton.onClick.AddListener(ContinueGame);
        newGameButton.onClick.AddListener(StartNewGame);

        // 初始化时隐藏确认面板
        confirmPanel.alpha = 0;
        confirmPanel.interactable = false;
        confirmPanel.blocksRaycasts = false;
    }

    private void StartButton()
    {
        // 检查是否有未完成的游戏
        if (SaveManager.Instance != null && SaveManager.Instance.HasContinueGame())
        {
            // 显示确认面板
            ShowConfirmPanel();
        }
        else
        {
            // 没有未完成的游戏，直接跳转到角色选择场景
            SceneManager.LoadScene("02-LevelSelect");
        }
    }

    private void ShowConfirmPanel()
    {
        // 显示确认面板
        confirmPanel.alpha = 1;
        confirmPanel.interactable = true;
        confirmPanel.blocksRaycasts = true;
    }

    private void HideConfirmPanel()
    {
        // 隐藏确认面板
        confirmPanel.alpha = 0;
        confirmPanel.interactable = false;
        confirmPanel.blocksRaycasts = false;

    }

    // 继续游戏
    private void ContinueGame()
    {
        // 恢复游戏进度到GameManage
        if (SaveManager.Instance != null && SaveManager.Instance.HasContinueGame())
        {
            // 恢复数据到GameManage
            if (GameManage.Instance != null)
            {
                GameManage.Instance.RestoreGame();
            }

        }
        SceneManager.LoadScene(2);
    }

    // 开始新游戏
    private void StartNewGame()
    {
        // 清除当前游戏标记
        if (SaveManager.Instance != null)
        {
            GameProgressData data = SaveManager.Instance.GetProgressData();
            data.hasCurrentGame = false;
            SaveManager.Instance.SaveProgress();
        }
        // 跳转到角色选择场景
        SceneManager.LoadScene("02-LevelSelect");
    }

    private void ProgressButton()
    {
        mainMenuPanel.alpha = 0;
        mainMenuPanel.interactable = false;
        mainMenuPanel.blocksRaycasts = false;

        if (ProgressPanel.instance != null)
        {
            ProgressPanel.instance._progressPanel.alpha = 1;
            ProgressPanel.instance._progressPanel.interactable = true;
            ProgressPanel.instance._progressPanel.blocksRaycasts = true;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isMainmemu)
        {
            // 如果确认面板正在显示，按ESC返回主菜单
            if (confirmPanel.alpha > 0)
            {
                HideConfirmPanel();
            }
            else
            {
                ExiteButton();
            }
        }
    }

    private void ExiteButton()
    {
        // 退出前自动保存
        if (GameManage.Instance != null && GameManage.Instance.currentRole != null)
        {
            GameManage.Instance.SaveGame();
        }

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}