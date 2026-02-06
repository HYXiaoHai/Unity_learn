using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using Unity.VisualScripting;

public class PropsDetailsUI : MonoBehaviour
{
    public Image image;//头像
    public TMP_Text nameTxt;//名字
    public TMP_Text descriptionTxt;//道具描述
    private int currentId; // 当前商品的ID
    private bool currentIsWeapon; // 当前是否是武器
    private int instanceId; // 每个商品实例的唯一标识

    [Header("购买按钮相关")]
    public Button buy;//购买按钮
    public TMP_Text needCount;//需要的金币
    public Image buyButtonImage; // 购买按钮的背景Image（不是needCount_BG）

    [Header("锁定按钮相关")]
    public Button lockthis;//锁定按钮
    public Image lockButtonImage; // 锁定按钮的背景Image（不是lock_BG）
    public bool isLock;//是否被锁定。

    public PropData propdata;
    public WeaponData weapondata;
    private void Start()
    {
        // 生成唯一实例ID
        instanceId = System.Guid.NewGuid().GetHashCode();

        if (!currentIsWeapon)//不是武器
        {
            // 计算并显示折扣价格
            float discountPrice = propdata.price * (1 + GameManage.Instance.shopDiscount);
            int finalPrice = Mathf.FloorToInt(discountPrice);
            needCount.text = finalPrice.ToString();
        }
        else
        {
            // 计算并显示折扣价格
            float discountPrice = weapondata.price * (1 + GameManage.Instance.shopDiscount);
            int finalPrice = Mathf.FloorToInt(discountPrice);
            needCount.text = finalPrice.ToString();
        }

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
            {
            if (!currentIsWeapon)//道具购买
                StartCoroutine(UseAttaick());
            else//武器购买
                StartCoroutine(Buyweapon());
            });

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

        // 允许重复锁定，直接添加
        ShopLock newLock = new ShopLock { isweapon = currentIsWeapon, id = currentId };
        GameManage.Instance.lockedPropIds.Add(newLock);

        isLock = true;
        lockButtonImage.color = Color.red;
        Debug.Log($"已锁定: {(currentIsWeapon ? "武器" : "道具")} ID:{currentId} (实例:{instanceId})");
    }

    void UnlockThisProp()
    {
        // 由于允许重复锁定，我们需要移除一个匹配的锁定
        // 遍历锁定列表，找到第一个匹配的并移除
        for (int i = 0; i < GameManage.Instance.lockedPropIds.Count; i++)
        {
            var lockItem = GameManage.Instance.lockedPropIds[i];
            if (lockItem.isweapon == currentIsWeapon && lockItem.id == currentId)
            {
                GameManage.Instance.lockedPropIds.RemoveAt(i);
                isLock = false;
                lockButtonImage.color = Color.black;
                Debug.Log($"已解锁: {(currentIsWeapon ? "武器" : "道具")} ID:{currentId} (实例:{instanceId})");
                return;
            }
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
    public void Initprop(PropData propData)
    {
        propdata = propData;
        currentIsWeapon = false;
        currentId = propData.id;
        image.sprite = Resources.Load<SpriteAtlas>("Image/其他/Props").GetSprite(propdata.name);
            nameTxt.text = propdata.name;
            descriptionTxt.text = propdata.describe;
    }
    //初始化武器
    public void Initweapon(WeaponData weaponData)
    {
        weapondata = weaponData;
        currentIsWeapon = true;
        currentId = weaponData.id;
        image.sprite = Resources.Load<Sprite>(weaponData.avatar);
            nameTxt.text = weapondata.name;
            descriptionTxt.text = weapondata.describe;
    }
    // 设置锁定状态（由 PropsSelectPanel 调用）
    public void SetLocked(bool locked)
    {
        isLock = locked;
        lockButtonImage.color = locked ? Color.red : Color.black;
    }
    //应用属性（武器购买逻辑）
    IEnumerator Buyweapon()
    {
        // 计算价钱
        float discountPrice = weapondata.price * (1 + GameManage.Instance.shopDiscount);
        int finalPrice = Mathf.FloorToInt(discountPrice);

        if (finalPrice > GameManage.Instance.currentMoney)
        {
            yield return StartCoroutine(ShowNotEnoughMoneyEffect());
            yield break;
        }

        bool isBagFull = GameManage.Instance.currentWeapon.Count >= 6;

        if (isBagFull)
        {
            // 背包已满，尝试自动合成
            if (GameManage.Instance.CanAutoMergeWeapon(weapondata.id, weapondata.grade))
            {
                // 执行自动合成
                if (GameManage.Instance.TryAutoMergeWeapon(weapondata.id, weapondata.grade, weapondata))
                {
                    // 合成成功
                    Debug.Log($"自动合成成功！");
                }
                else
                {
                    Debug.LogWarning("自动合成失败");
                    yield break;
                }
            }
            else
            {
                yield break;
            }
        }
        else
        {
            // 背包未满，直接添加
            GameManage.Instance.currentWeapon.Add(weapondata);
        }

        // 如果是锁定的道具，先解锁
        if (isLock)
        {
            GameManage.Instance.TryRemoveLock(true, weapondata.id);
        }

        // 扣款
        GameManage.Instance.currentMoney -= finalPrice;

        // 更新金币显示
        PropsSelectPanel.instance.RenewMoney();
        PropsSelectPanel.instance.RenewWeaponUI();//更新面板

        yield return null;
        Destroy(gameObject);
    }

    //应用属性（道具购买逻辑）
    IEnumerator UseAttaick()
    {
        float discountPrice = propdata.price * (1 + GameManage.Instance.shopDiscount);
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
            GameManage.Instance.TryRemoveLock(false, propdata.id);
        }
        //购买
        GameManage.Instance.currentProp.Add(propdata);//加入已选择数组
        GameManage.Instance.currentMoney -= finalPrice;

        ApplyAttributeBonuses();

        // 更新UI
        PropsSelectPanel.instance.Renewattribute();//更新面板
        PropsSelectPanel.instance.RenewMoney();//更新面板
        PropsSelectPanel.instance.RenewPropsUI();//更新面板

        yield return null;

        Destroy(gameObject);
    }

    // 应用属性加成
    void ApplyAttributeBonuses()
    {
        GameManage.Instance.currentAttribute.maxHealth += propdata.maxHP;
        GameManage.Instance.currentAttribute.healthRegeneration += propdata.revive;
        GameManage.Instance.currentAttribute.rangedDamage += propdata.long_damage * GameManage.Instance.currentAttribute.rangedDamage;
        GameManage.Instance.currentAttribute.meleeDamage += propdata.short_damage * GameManage.Instance.currentAttribute.meleeDamage;
        GameManage.Instance.currentAttribute.range += propdata.short_range;
        GameManage.Instance.currentAttribute.range += propdata.long_range;
        GameManage.Instance.currentAttribute.attackSpeedPercent += propdata.short_attackSpeed * GameManage.Instance.currentAttribute.attackSpeedPercent;
        GameManage.Instance.currentAttribute.attackSpeedPercent += propdata.long_attackSpeed * GameManage.Instance.currentAttribute.attackSpeedPercent;
        GameManage.Instance.currentAttribute.speedPercent += propdata.speed;
        GameManage.Instance.currentAttribute.harvest += propdata.harvest;
        GameManage.Instance.currentAttribute.dodgePercent += propdata.speedPer;
        GameManage.Instance.currentAttribute.range += propdata.pickRange;
        GameManage.Instance.currentAttribute.criticalRate += propdata.critical_strikes_probability;
        GameManage.Instance.slot += propdata.slot;
        GameManage.Instance.shopDiscount += propdata.shopDiscount;
        GameManage.Instance.expMuti += propdata.expMuti;
    }
}