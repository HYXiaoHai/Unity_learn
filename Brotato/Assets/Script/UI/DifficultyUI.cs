using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[Serializable]
public class DifficultyUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public DifficutyData _difficutyData;

    public Image _backImage;//背景
    public Image _avatar;//头像
    public Button _button;//按钮

    private AudioSource audioSource;
    private AudioClip audioClip;
    private void Awake()
    {
        _backImage = GetComponent<Image>();
        _avatar = transform.GetChild(0).GetComponent<Image>();
        _button = GetComponent<Button>();

        audioSource = GetComponent<AudioSource>();
        audioClip = Resources.Load<AudioClip>("Music/菜单音效");
    }
    public void SetData(DifficutyData data)
    {
        _difficutyData = data;
        int n = data.id - 1;
        _avatar.sprite = Resources.Load<SpriteAtlas>("Image/UI/危险等级").GetSprite(_difficutyData.name);

        SetBackColor(_difficutyData.id);

        _button.onClick.AddListener(() =>
        {
            //记录难度
            GameManage.Instance.currentDifficulty = data;
            //初始化开始游戏
            GameManage.Instance.NewGame();
            //跳转场景
            SceneManager.LoadScene(2);
        }
        );
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        _backImage.color = new Color(207 / 255f, 207 / 255f, 207 / 255f);
        audioSource.PlayOneShot(audioClip);

        if (DifficultySelectPanel.Instance._difficultyDetallscanvasGroup.alpha != 1)
            DifficultySelectPanel.Instance._difficultyDetallscanvasGroup.alpha = 1;//显示大屏

        //设置上方面板
        ShowUI(_difficutyData);
    }
    void ShowUI(DifficutyData d)
    {
        //修改武器名称
        DifficultySelectPanel.Instance._difficultyName.text = d.levelName;
        DifficultySelectPanel.Instance._difficultyavatar.sprite = Resources.Load<SpriteAtlas>("Image/UI/危险等级").GetSprite(_difficutyData.name);
        //修改武器描述
        DifficultySelectPanel.Instance._difficultyDescribe.text = GetdifficultyDescribe();
    }

    string GetdifficultyDescribe()
    {
        string result = "";

        foreach(DifficutyData data in DifficultySelectPanel.Instance._difficultyDatas)
        {
            result += data.describe+"\n";
            if(data == _difficutyData)
            {
                break;
            }
        }

        //当前结果是危险0
        if(result == "\n")
        {
            result = "无修改";
        }
        else//危险1-5
        {
            result = result.TrimStart('\n');//修建
        }
            return result;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SetBackColor(_difficutyData.id);
    }
    public void SetBackColor(int id)
    {
        switch (id)
        {
            case 1:
                _backImage.color = new Color(33 / 255f, 33 / 255f, 33 / 255f); break;
            case 2:
                _backImage.color = new Color(63 / 255f, 88 / 255f, 104 / 255f); break;
            case 3:
                _backImage.color = new Color(83 / 255f, 62 / 255f, 103 / 255f); break;
            case 4:
                _backImage.color = new Color(103 / 255f, 54 / 255f, 54 / 255f); break;
            case 5:
                _backImage.color = new Color(103 / 255f, 69 / 255f, 54 / 255f); break;
            case 6:
                _backImage.color = new Color(91 / 255f, 87 / 255f, 55 / 255f); break;
        }

    }

}
