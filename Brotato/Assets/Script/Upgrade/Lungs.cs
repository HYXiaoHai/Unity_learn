using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lungs : UpgradeBase
{
    public override void Start()
    {
        val = 3;//默认3
        base.Start();
    }

    public override void ButtonClick()
    {
        //增加数值
        GameManage.Instance.currentAttribute.healthRegeneration += val;

        //修改属性面板
        base.ButtonClick();
    }
}
