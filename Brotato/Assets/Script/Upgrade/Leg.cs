using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
//+3%速度
//+6%速度
//+9%速度
//+12%速度
public class Leg : UpgradeBase
{
    public override void Start()
    {
        val = 3;//默认3
       base.Start();
    }

    public override void ButtonClick()
    {
        //增加数值
        GameManage.Instance.currentAttribute.speedPercent += val;
        //修改属性面板
        base.ButtonClick();
    }
}
