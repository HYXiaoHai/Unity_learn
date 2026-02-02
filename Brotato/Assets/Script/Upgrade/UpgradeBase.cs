using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpgradeBase : MonoBehaviour
{
    public int val = 3;//数值
    public int level;//等级
    public Button _button;//按钮
 
    private void Awake()
    {
        _button = GetComponentInChildren<Button>();
    }
    public virtual void Start()
    {
        _button.onClick.AddListener(() =>
        {
            ButtonClick();
        }
       );
    }

    public void Init(int lv)
    {
        level = lv;
        //根据lv初始化数据
    }

    public virtual void ButtonClick()
    {

        //增加数值

        //修改属性面板
        UpgradePanel.instance.Renewattribute();
        //减少次数
        LevelController.Instance.expUpCount--;
        GameObject g =  Player.instance.expUPimagePrafbs.Pop();
        Destroy( g );
        if (LevelController.Instance.expUpCount == 0)
        {
            //跳转场景
            GoShop();
        }
        else
        {
            UpgradePanel.instance.RenewUpgradel();
        }
    }
    void GoShop()
    {
        SceneManager.LoadScene(3);
    }
}
