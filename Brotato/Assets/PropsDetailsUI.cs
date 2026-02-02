using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

public class PropsDetailsUI : MonoBehaviour
{
    public Image image;//头像
    public TMP_Text nameTxt;//名字
    public TMP_Text descriptionTxt;//道具描述

    [Header("购买按钮相关")]
    public Button buy;//购买按钮
    public TMP_Text needCount;//需要的金币
    public Image buyButtonImage; // 购买按钮的背景Image（不是needCount_BG）

    [Header("锁定按钮相关")]
    public Button lockthis;//锁定按钮
    public Image lockButtonImage; // 锁定按钮的背景Image（不是lock_BG）
    public bool isLock;//是否被锁定。

    public PropData data;

    private void Start()
    {
        // 计算并显示折扣价格
        float discountPrice = data.price * (1 + GameManage.Instance.shopDiscount);
        int finalPrice = Mathf.FloorToInt(discountPrice);
        needCount.text = finalPrice.ToString();

        // 配置购买按钮的颜色过渡
        ColorBlock buyColors = buy.colors;
        buyColors.normalColor = new Color(150 / 255f, 150 / 255f, 150 / 255f, 1f);
        buyColors.highlightedColor = new Color(240 / 255f, 240 / 255f, 240 / 255f, 1f);
        buyColors.pressedColor = new Color(180 / 255f, 180 / 255f, 180 / 255f, 1f);
        buy.colors = buyColors;

        // 配置锁定按钮的颜色过渡
        ColorBlock lockColors = lockthis.colors;
        lockColors.normalColor = new Color(150 / 255f, 150 / 255f, 150 / 255f, 1f);
        lockColors.highlightedColor = new Color(240 / 255f, 240 / 255f, 240 / 255f, 1f);
        lockColors.pressedColor = new Color(180 / 255f, 180 / 255f, 180 / 255f, 1f);
        lockthis.colors = lockColors;

        buy.onClick.AddListener(() =>
            StartCoroutine(UseAttaick())
        );

        lockthis.onClick.AddListener(() =>
        {
            if (isLock)
            {
                // 如果已经锁定，执行解锁
                UnlockThisProp();
            }
            else
            {
                // 如果未锁定，尝试锁定
                LockThisProp();
            }
        });
    }
    void LockThisProp()
    {
        // 检查是否达到最大锁定数量
        if (GameManage.Instance.lockedPropIds.Count >= 4)
        {
            Debug.LogWarning("已达到最大锁定数量(4个)");
            return;
        }

        // 添加到锁定列表
        GameManage.Instance.lockedPropIds.Add(data.id);
        isLock = true;
        lockButtonImage.color = Color.red;
        Debug.Log($"已锁定道具: {data.name}");
    }

    void UnlockThisProp()
    {
        // 从锁定列表中移除（只移除第一个匹配项）
        if (GameManage.Instance.lockedPropIds.Contains(data.id))
        {
            int indexToRemove = GameManage.Instance.lockedPropIds.IndexOf(data.id);
            GameManage.Instance.lockedPropIds.RemoveAt(indexToRemove);

            isLock = false;
            lockButtonImage.color = Color.black;
            Debug.Log($"已解锁道具: {data.name}");
        }
    }
    IEnumerator ShowNotEnoughMoneyEffect()
    {
        if (buyButtonImage != null)
        {
            Color originalColor = buyButtonImage.color;
            for (int i = 0; i < 3; i++)
            {
                buyButtonImage.color = Color.red;
                yield return new WaitForSeconds(0.1f);
                buyButtonImage.color = originalColor;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
    //初始化
    public void Init(PropData propData)
    {
        data = propData;

        image.sprite = Resources.Load<SpriteAtlas>("Image/其他/Props").GetSprite(data.name);
        nameTxt.text = data.name;
        descriptionTxt.text = data.describe;
    }
    // 设置锁定状态（由 PropsSelectPanel 调用）
    public void SetLocked(bool locked)
    {
        isLock = locked;
        lockButtonImage.color = locked ? Color.red : Color.black;
    }
    //应用属性
    IEnumerator UseAttaick()
    {
        float discountPrice = data.price * (1 + GameManage.Instance.shopDiscount);
        int finalPrice = Mathf.FloorToInt(discountPrice);
        if (finalPrice > GameManage.Instance.currentMoney)
        {
            yield return StartCoroutine(ShowNotEnoughMoneyEffect());
            yield break;
        }

        // 如果是锁定的道具，先解锁
        if (isLock)
        {
            // 从锁定列表中移除
            if (GameManage.Instance.lockedPropIds.Contains(data.id))
            {
                int indexToRemove = GameManage.Instance.lockedPropIds.IndexOf(data.id);
                GameManage.Instance.lockedPropIds.RemoveAt(indexToRemove);
            }
        }
        //购买
        GameManage.Instance.currentProp.Add(data);//加入已选择数组
        GameManage.Instance.currentMoney -= finalPrice;

        ApplyAttributeBonuses();

        // 更新UI
        PropsSelectPanel.instance.Renewattribute();//更新面板
        PropsSelectPanel.instance.RenewMoney();//更新面板

        yield return null;

        Destroy(gameObject);
    }

    // 应用属性加成
    void ApplyAttributeBonuses()
    {


        GameManage.Instance.currentAttribute.maxHealth += data.maxHP;
        GameManage.Instance.currentAttribute.healthRegeneration += data.revive;
        GameManage.Instance.currentAttribute.rangedDamage += data.long_damage * GameManage.Instance.currentAttribute.rangedDamage;
        GameManage.Instance.currentAttribute.meleeDamage += data.short_damage * GameManage.Instance.currentAttribute.meleeDamage;
        GameManage.Instance.currentAttribute.range += data.short_range;
        GameManage.Instance.currentAttribute.range += data.long_range;
        GameManage.Instance.currentAttribute.attackSpeedPercent += data.short_attackSpeed * GameManage.Instance.currentAttribute.attackSpeedPercent;
        GameManage.Instance.currentAttribute.attackSpeedPercent += data.long_attackSpeed * GameManage.Instance.currentAttribute.attackSpeedPercent;
        GameManage.Instance.currentAttribute.speedPercent += data.speed;
        GameManage.Instance.currentAttribute.harvest += data.harvest;
        GameManage.Instance.currentAttribute.dodgePercent += data.speedPer;
        GameManage.Instance.currentAttribute.range += data.pickRange;
        GameManage.Instance.currentAttribute.criticalRate += data.critical_strikes_probability;
        GameManage.Instance.slot += data.slot;
        GameManage.Instance.shopDiscount += data.shopDiscount;
        GameManage.Instance.expMuti += data.expMuti;
    }
}