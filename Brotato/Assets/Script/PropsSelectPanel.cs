using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
// 辅助类：表示商店中的一个商品
public class ShopItem
{
    public bool isWeapon; // 是否是武器
    public int id; // 商品ID
    public bool isLocked; // 是否被锁定
}
public class PropsSelectPanel : MonoBehaviour
{
    public  static PropsSelectPanel instance;

    [Header("商店面板")]
    public int haveMoneycount;//总共有的金币数目
    public TMP_Text _haveMoney;
    public TMP_Text _title;//标题 “商店(第2波)”
    public Button _nextWave;//下一关
    public TMP_Text _nextWaveText;//波数“出发(第*波)”
    public Button _refresh;//刷新
    public Button _sell;//卖道具


    public List<PropData> propDatas = new List<PropData>();//道具data信息
    public TextAsset PropsTextAsset;//道具json
    public List<WeaponData> weponDatas = new List<WeaponData>();//道具data信息
    public TextAsset weaponsTextAsset;//武器json
    public GameObject prop_fabs;//商品细节面板预制体（武器与道具公用）

    public List<GameObject> currentProps = new List<GameObject>();//当前展示的商品

    [Header("展示面板")]
    public ShopWeaponUI weapon;

    public GameObject weaponUi_prafbs;//ui list结构体
    public GameObject propUi_prafbs;//ui list结构体
    public Transform _weaponList;
    public Transform _propList;

    public List<GameObject> pro_fbs = new List<GameObject>();
    public List<GameObject> wea_fbs = new List<GameObject>();
    public Transform _propsContent;

    public Button weaponListButton;//武器展示面板
    public Button propsListButton;//道具展示面板
    public Button weaponMergeButton;//武器升级按钮

    public GameObject selectedWeaponUI; // 当前选中的武器UI对象
    public WeaponData selectedWeaponData; // 当前选中的武器数据

    private bool isWeaponList;
    private bool isPropList;
    [Header("属性界面")]
    public GameObject _attribute;//属性界面
    public AttributeData _attributeData;//属性
    // 基础属性
    public TMP_Text currentLevelText;               // 当前等级
    public TMP_Text maxHealthText;                 // 最大生命值
    public TMP_Text healthRegenerationText;        // 生命再生（每秒恢复量）
    public TMP_Text lifeStealPercentText;          // %生命窃取（0-1，表示百分比）
    public TMP_Text meleeDamageText;               // 近战伤害
    public TMP_Text damagePercentText;             // %伤害
    public TMP_Text rangedDamageText;              // 远程伤害
    public TMP_Text elementalDamageText;           // 元素伤害
    public TMP_Text criticalRateText;              // 暴击率
    public TMP_Text engineeringText;               // 工程学（可能是召唤物伤害或数量）
    public TMP_Text attackSpeedPercentText;        // %攻击速度（0-1）
    public TMP_Text rangeText;                     // 攻击范围
    public TMP_Text armorText;                     // 护甲值
    public TMP_Text dodgePercentText;              // %闪避率（0-1）
    public TMP_Text speedPercentText;              // %移动速度（0-1）
    public TMP_Text luckText;                      // 幸运值（可能影响掉落率）
    public TMP_Text harvestText;                   // 收获（采集效率）
    private void Awake()
    {
        instance = this;

        _haveMoney = GameObject.Find("HaveMoney").GetComponent<TMP_Text>();
        _title = GameObject.Find("Tittle").GetComponent<TMP_Text>();
        _nextWave = GameObject.Find("NextWave").GetComponent<Button>();
        _nextWaveText = GameObject.Find("NextWaveText").GetComponent<TMP_Text>();
        _refresh = GameObject.Find("Refresh").GetComponent<Button>();
        _sell = GameObject.Find("Sell").GetComponent<Button>();
        
        //json
        PropsTextAsset = Resources.Load<TextAsset>("Data/prop");//读取道具
        propDatas = JsonConvert.DeserializeObject<List<PropData>>(PropsTextAsset.text);

        weaponsTextAsset = Resources.Load<TextAsset>("Data/weapon");//读取weapon
        weponDatas = JsonConvert.DeserializeObject<List<WeaponData>>(weaponsTextAsset.text);

        prop_fabs = Resources.Load<GameObject>("Prefabs/PropsDetail");
        
        //
        _propsContent = GameObject.Find("PropsContent").GetComponent<Transform>();
        _weaponList = GameObject.Find("WeaponList").GetComponent<Transform>();
        _propList = GameObject.Find("PropList").GetComponent<Transform>();

        weaponUi_prafbs = Resources.Load<GameObject>("Prefabs/ShopWeaponUI");
        propUi_prafbs = Resources.Load<GameObject>("Prefabs/ShopPropUI");

        weaponListButton = GameObject.Find("Wuqi").GetComponent<Button>();
        propsListButton = GameObject.Find("Daoju").GetComponent<Button>();
        weaponMergeButton = GameObject.Find("Shengji").GetComponent<Button>();

        // 确保升级按钮初始状态为不可见
        if (weaponMergeButton != null)
        {
            weaponMergeButton.gameObject.SetActive(false);
        }

        //初始化属性面板
        currentLevelText = GameObject.Find("CurrentLevel1").GetComponentInChildren<TMP_Text>();
        maxHealthText = GameObject.Find("MaxHP").GetComponentInChildren<TMP_Text>();
        healthRegenerationText = GameObject.Find("HpAgain").GetComponentInChildren<TMP_Text>();
        lifeStealPercentText = GameObject.Find("LifeSteal").GetComponentInChildren<TMP_Text>();
        meleeDamageText = GameObject.Find("MeleeDamage").GetComponentInChildren<TMP_Text>();
        damagePercentText = GameObject.Find("Damage").GetComponentInChildren<TMP_Text>();
        rangedDamageText = GameObject.Find("RangedDamage").GetComponentInChildren<TMP_Text>();
        elementalDamageText = GameObject.Find("MagakeDamage").GetComponentInChildren<TMP_Text>();
        criticalRateText = GameObject.Find("CriticalHit").GetComponentInChildren<TMP_Text>();
        engineeringText = GameObject.Find("Engineering").GetComponentInChildren<TMP_Text>();
        attackSpeedPercentText = GameObject.Find("AttackSpeed").GetComponentInChildren<TMP_Text>();
        rangeText = GameObject.Find("Scope").GetComponentInChildren<TMP_Text>();
        armorText = GameObject.Find("Armor").GetComponentInChildren<TMP_Text>();
        dodgePercentText = GameObject.Find("Dodge").GetComponentInChildren<TMP_Text>();
        speedPercentText = GameObject.Find("Speed").GetComponentInChildren<TMP_Text>();
        luckText = GameObject.Find("Lucky").GetComponentInChildren<TMP_Text>();
        harvestText = GameObject.Find("Harvest").GetComponentInChildren<TMP_Text>();
    }
    private void Start()
    {
        _title.text = "商店(第"+GameManage.Instance.currentWave.ToString() + "波)";
        _nextWaveText.text = "出发(第" + (GameManage.Instance.currentWave+1).ToString()+ "波)";
        
        isWeaponList = true;//默认武器list
        
        RenewProp();
        RenewMoney();
        RenewPropsUI();
        RenewWeaponUI();
        Renewattribute();

        _refresh.onClick.AddListener(() =>
          Refresh()
        );
        _nextWave.onClick.AddListener(() =>
          Nextwave()
        );
        weaponListButton.onClick.AddListener(()=>
            OnweaponListButton()
        );
        propsListButton.onClick.AddListener(()=>
            OnpropsListButton()
        );
        weaponMergeButton.onClick.AddListener(()=>
            OnweaponMergeButton()
        );
        _sell.onClick.AddListener(()=>
            OnSellButton()
        );
    }
    //卖道具按钮(只能卖武器)
    void OnSellButton()
    {
        if(isWeaponList&&selectedWeaponUI!=null)
        {
            //卖
            GameManage.Instance.SellWeapon(selectedWeaponData);
            RenewWeaponUI();
            RenewMoney();
        }
        else
        {
            Debug.Log(isWeaponList);
            Debug.Log(isPropList);
            Debug.Log(selectedWeaponUI);
            StartCoroutine(ShowNotEnoughSellEffect());
        }
    }
    //卖失败（禁止卖）
    IEnumerator ShowNotEnoughSellEffect()
    {
        if (_sell.image != null)
        {
            Color originalColor = Color.black;
            for (int i = 0; i < 3; i++)
            {
                _sell.image.color = Color.red;
                yield return new WaitForSeconds(0.1f);
                _sell.image.color = originalColor;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
    //随机道具（data信息）
    void Refresh()
    {
        if (GameManage.Instance.currentMoney - 4 < 0)
        {
            //不足三金币
            return;
        }
        GameManage.Instance.currentMoney -= 4;//
        RenewMoney();//刷新金币显示
        RenewProp();//刷新选择面板
    }
    //更新金币
    public void RenewMoney()
    {
        _haveMoney.text = GameManage.Instance.currentMoney.ToString();
    }
    //刷新购买面板
    public void RenewProp()
    {
        ClearCurrentProps();

        // 创建商品列表（最多4个）
        List<ShopItem> shopItems = new List<ShopItem>();

        // 先添加锁定的商品
        foreach (var lockItem in GameManage.Instance.lockedPropIds)
        {
            if (shopItems.Count >= 4) break;

            ShopItem item = new ShopItem
            {
                isWeapon = lockItem.isweapon,
                id = lockItem.id,
                isLocked = true
            };
            shopItems.Add(item);
        }

        // 计算还需要生成多少个商品
        int needCount = 4 - shopItems.Count;

        // 随机生成剩余的商品
        for (int i = 0; i < needCount; i++)
        {
            // 随机决定是武器还是道具（各50%概率）
            bool isWeapon = Random.Range(0, 2) == 0;
            int randomId;

            if (isWeapon)
            {
                // 从武器数据中随机选择一个
                int randomIndex = Random.Range(0, weponDatas.Count);
                randomId = weponDatas[randomIndex].id;
            }
            else
            {
                // 从道具数据中随机选择一个
                int randomIndex = Random.Range(0, propDatas.Count);
                randomId = propDatas[randomIndex].id;
            }
            ShopItem item = new ShopItem
            {
                isWeapon = isWeapon,
                id = randomId,
                isLocked = false
            };
            shopItems.Add(item);
        }

        //实例化商品面板
        foreach (var item in shopItems)
        {
            PropsDetailsUI p = Instantiate(prop_fabs, _propsContent).GetComponent<PropsDetailsUI>();

            if (item.isWeapon)
            {
                // 查找武器数据
                WeaponData weaponData = weponDatas.Find(w => w.id == item.id);
                if (weaponData != null)
                {   
                    p.Initweapon(weaponData);
                }
            }
            else
            {
                // 查找道具数据
                PropData propData = propDatas.Find(pd => pd.id == item.id);
                if (propData != null)
                {
                    p.Initprop(propData);
                }
            }

            // 设置锁定状态
            if (item.isLocked)
            {
                p.SetLocked(true);
            }

            currentProps.Add(p.gameObject);
        }
    }

    //清理currentProps
    public void ClearCurrentProps()
    {
        foreach (GameObject p in currentProps)
        {
            if (p != null)
            {
                Destroy(p);
            }
        }
        currentProps.Clear();
    }

    //更新属性
    public void Renewattribute()
    {
        _attributeData = GameManage.Instance.currentAttribute;
        if (currentLevelText == null)
        {
            Debug.Log("error");
        }
        //初始化属性面板
        currentLevelText.text = _attributeData.currentLevel.ToString();
        maxHealthText.text = _attributeData.maxHealth.ToString();
        healthRegenerationText.text = _attributeData.healthRegeneration.ToString();
        lifeStealPercentText.text = _attributeData.lifeStealPercent.ToString();
        meleeDamageText.text = _attributeData.meleeDamage.ToString();
        damagePercentText.text = _attributeData.damagePercent.ToString();
        rangedDamageText.text = _attributeData.rangedDamage.ToString();
        elementalDamageText.text = _attributeData.elementalDamage.ToString();
        criticalRateText.text = _attributeData.criticalRate.ToString();
        engineeringText.text = _attributeData.engineering.ToString();
        attackSpeedPercentText.text = _attributeData.attackSpeedPercent.ToString();
        rangeText.text = _attributeData.range.ToString();
        armorText.text = _attributeData.armor.ToString();
        dodgePercentText.text = _attributeData.dodgePercent.ToString();
        speedPercentText.text = _attributeData.speedPercent.ToString();
        luckText.text = _attributeData.luck.ToString();
        harvestText.text = _attributeData.harvest.ToString();
    }
    //更新武器list ui
    public void RenewWeaponUI()
    {
        ClearnWeaponUI();

        // 先隐藏升级按钮
        ShowWeaponMergeButton(false);

        foreach (WeaponData item in GameManage.Instance.currentWeapon)
        {
            ShopWeaponUI w = Instantiate(weaponUi_prafbs, _weaponList).GetComponent<ShopWeaponUI>();
            w.SetData(item);
            wea_fbs.Add(w.gameObject);

            // 如果这个武器和之前选中的武器数据匹配，设置选中状态
            if (selectedWeaponData != null &&
                item.id == selectedWeaponData.id &&
                item.grade == selectedWeaponData.grade)
            {
                // 找到第一个匹配的武器，设置为选中
                SetSelectedWeapon(w.gameObject, item);
                break;
            }
        }

        // 检查是否可以合成并更新按钮状态
        UpdateWeaponMergeButtonState();
    }
    //更新道具list ui
    public void RenewPropsUI()
    {
        ClearnPropUI();
        foreach (PropData item in GameManage.Instance.currentProp)
        {
            ShopPropUI s = Instantiate(propUi_prafbs,_propList ).GetComponent<ShopPropUI>();
            s.SetData(item);
            pro_fbs.Add(s.gameObject);
        }
    }
    //清理武器list ui
    void ClearnWeaponUI()
    {
        // 先清空选中状态
        ClearSelectedWeapon();
        foreach (var item in wea_fbs)
        {
            Destroy(item);
        }
        wea_fbs.Clear();
    }
    //清理道具list ui
    void ClearnPropUI()
    {
        foreach (var item in pro_fbs)
        {
            Destroy(item);
        }
        pro_fbs.Clear();
    }
    void OpenWeaponList()
    {
        isWeaponList = true;
        isPropList = false;
        CanvasGroup c = _weaponList.GetComponent<CanvasGroup>();
        c.alpha = 1.0f;
        c.interactable = true;
        c.blocksRaycasts = true;

        // 切换到武器列表时，检查是否有选中的武器并更新按钮状态
        if (selectedWeaponUI != null)
        {
            UpdateWeaponMergeButtonState();
        }
    }
    void CloseWeaponList()
    {
        isWeaponList = false;
        CanvasGroup c = _weaponList.GetComponent<CanvasGroup>();
        c.alpha = 0f;
        c.interactable = false;
        c.blocksRaycasts = false;
    }
    void OpenPropList()
    {
        isWeaponList = false;
        isPropList = true;
        CanvasGroup c = _propList.GetComponent<CanvasGroup>();
        c.alpha = 1.0f;
        c.interactable = true;
        c.blocksRaycasts = true;

        // 切换到道具列表时，隐藏升级按钮
        ShowWeaponMergeButton(false);
    }
    void ClosePropList()
    {
        isPropList = false;
        CanvasGroup c = _propList.GetComponent<CanvasGroup>();
        c.alpha = 0f;
        c.interactable = false;
        c.blocksRaycasts = false;
    }
    //按下武器列表ui按钮
    void OnweaponListButton()
    {
        ClosePropList();
        OpenWeaponList();
    }
    //按下道具列表ui按钮
    void OnpropsListButton()
    {
        CloseWeaponList();
        OpenPropList();
    }
    // 按下武器升级ui按钮
    void OnweaponMergeButton()
    {
        if (selectedWeaponData != null)
        {
            // 尝试合成武器
            if (GameManage.Instance.TryMergeWeapon(selectedWeaponData.id, selectedWeaponData.grade))
            {
                // 刷新武器UI
                RenewWeaponUI();

                // 隐藏升级按钮
                ShowWeaponMergeButton(false);

                // 清空选中状态
                ClearSelectedWeapon();
            }
            else
            {
                Debug.Log("武器升级失败");
            }
        }
    }

    // 设置选中的武器UI
    public void SetSelectedWeapon(GameObject weaponUI, WeaponData weaponData)
    {
        // 如果点击的是同一个武器UI，不执行任何操作（不取消选中）
        if (selectedWeaponUI == weaponUI)
        {
            return;
        }

        // 取消之前选中的武器UI的高亮
        if (selectedWeaponUI != null)
        {
            ShopWeaponUI oldWeaponUI = selectedWeaponUI.GetComponent<ShopWeaponUI>();
            if (oldWeaponUI != null)
            {
                oldWeaponUI.SetSelected(false);
            }
        }

        // 设置新的选中
        selectedWeaponUI = weaponUI;
        selectedWeaponData = weaponData;

        // 设置新选中的武器UI高亮
        ShopWeaponUI newWeaponUI = weaponUI.GetComponent<ShopWeaponUI>();
        if (newWeaponUI != null)
        {
            newWeaponUI.SetSelected(true);
        }

        // 更新升级按钮状态
        UpdateWeaponMergeButtonState();
    }

    // 清空选中的武器
    public void ClearSelectedWeapon()
    {
        // 取消之前选中的武器UI的高亮
        if (selectedWeaponUI != null)
        {
            ShopWeaponUI oldWeaponUI = selectedWeaponUI.GetComponent<ShopWeaponUI>();
            if (oldWeaponUI != null)
            {
                oldWeaponUI.SetSelected(false);
            }
        }

        selectedWeaponUI = null;
        selectedWeaponData = null;
    }

    // 更新武器升级按钮状态
    private void UpdateWeaponMergeButtonState()
    {
        if (selectedWeaponData != null)
        {
            // 检查是否可以合成
            if (GameManage.Instance.CanMergeWeapon(selectedWeaponData.id, selectedWeaponData.grade))
            {
                // 可以合成，显示升级按钮
                ShowWeaponMergeButton(true);
            }
            else
            {
                // 不可以合成，隐藏升级按钮
                ShowWeaponMergeButton(false);
            }
        }
        else
        {
            // 没有选中武器，隐藏升级按钮
            ShowWeaponMergeButton(false);
        }
    }

    // 显示或隐藏升级按钮
    public void ShowWeaponMergeButton(bool show)
    {
        if (weaponMergeButton != null)
        {
            weaponMergeButton.gameObject.SetActive(show);
        }
    }

    //下一关
    void Nextwave()
    {
        //重置关卡数据
        GameManage.Instance.NextWave();
        //开始下一关
        SceneManager.LoadScene(2);
    }
}
