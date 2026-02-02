using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    public static UpgradePanel instance;
    public CanvasGroup _upgradelPanel;//升级面板
    public TMP_Text _moneyCount;//金币文本
    [Header("升级界面")]
    Button _upgradelRefreshButton;//刷新按钮
    public GameObject _upgrade;//升级界面
    public List<GameObject> allUpgradePrefabs = new List<GameObject>(); //所有升级选项预制体
    private List<GameObject> selectedPrefabs = new List<GameObject>();//选择的预制体
    private List<Transform> fatherTransforms = new List<Transform>();//父亲的位置
    public Transform _u1;//升级的位置（实例化的父级）
    public Transform _u2;
    public Transform _u3;
    public Transform _u4;

    [Header("属性界面")]
    public GameObject _attribute;//属性界面
    public AttributeData _attributeData;//属性
    // 基础属性
    public TMP_Text currentLevelText;                // 当前等级
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

        _upgradelPanel = GameObject.Find("UpgradePanel").GetComponent<CanvasGroup>();
        _moneyCount = GameObject.Find("MoneyCount1").GetComponent<TMP_Text>();
        //升级
        _upgrade = GameObject.Find("Upgrade");
        _upgradelRefreshButton = GameObject.Find("UpgradeRefresh").GetComponent<Button>();
        _u1 = GameObject.Find("Upgrade1").GetComponent<Transform>();
        _u2 = GameObject.Find("Upgrade2").GetComponent<Transform>();
        _u3 = GameObject.Find("Upgrade3").GetComponent<Transform>();
        _u4 = GameObject.Find("Upgrade4").GetComponent<Transform>();

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
        LoadUpgradePrefabs();
        //初始化父级位置
        fatherTransforms.Add(_u1);
        fatherTransforms.Add(_u2);
        fatherTransforms.Add(_u3);
        fatherTransforms.Add(_u4);


        _upgradelRefreshButton.onClick.AddListener(() =>
        {
            Refresh();
        });
    }
    //加载所有预制体
    private void LoadUpgradePrefabs()
    {
        for (int i = 0; i < 7; i++) // 先拿7种做实验
        {
            string path = $"Prefabs/upgrade_{i}";
            GameObject prefab = Resources.Load<GameObject>(path);
            if (prefab != null)
            {
                allUpgradePrefabs.Add(prefab);
            }
            else
            {
                Debug.LogWarning($"无法加载预制体: {path}");
            }
        }

        if (allUpgradePrefabs.Count == 0)
        {
            Debug.LogError("没有加载到任何升级预制体！");
        }
    }

    //更新金币
    public void RenewMoney()
    {
        //_moneyCount.text = Player.instance.money.ToString();
        _moneyCount.text = GameManage.Instance.currentMoney.ToString();
    }

    /////////////升级/////////////
    //获取随机四个选项
    private int[] GetUniqueRandomIndices(int count, int min, int max)
    {
        List<int> indices = new List<int>();
        for (int i = min; i < max; i++)
        {
            indices.Add(i);
        }
        int[] result = new int[count];
        for (int i = 0; i < count; i++)
        {
            int rand = Random.Range(0, indices.Count);
            result[i] = indices[rand];
            indices.RemoveAt(rand);
        }
        return result;
    }
    //更新选择面板
    public void RenewUpgradel()
    {
        // 清理旧的实例
        ClearCurrentInstances();

        int[] RandomIndex = GetUniqueRandomIndices(4, 0, allUpgradePrefabs.Count);

        // 确保有足够的预制体
        if (RandomIndex.Length == 0 || RandomIndex.Length < 4)
        {
            Debug.LogError("无法获取足够的随机索引");
            return;
        }
        for (int i = 0; i < RandomIndex.Length; i++)
        {
            int randlevel = Random.Range(1, 5);//1~4级

            UpgradeBase u = Instantiate(allUpgradePrefabs[RandomIndex[i]], fatherTransforms[i]).GetComponent<UpgradeBase>();
            u.Init(randlevel);
            selectedPrefabs.Add(u.gameObject);//加入链表管理
        }
    }
    private void ClearCurrentInstances()
    {
        foreach (GameObject instance in selectedPrefabs)
        {
            if (instance != null)
            {
                Destroy(instance);
            }
        }
        selectedPrefabs.Clear();
    }
    //刷新
    void Refresh()
    {
        if(GameManage.Instance.currentMoney - 3<0)
        {
            //不足三金币
            return;
        }
        GameManage.Instance.currentMoney -= 3;//
        RenewMoney();//刷新金币显示
        GamePanel.instance.RenewMoney();//刷新屏幕中的金币数
        RenewUpgradel();//刷新选择面板
    }
    // 添加销毁时的清理
    private void OnDestroy()
    {
        ClearCurrentInstances();
    }


    ////////////////////////属性面板/////////////////////
    public void Renewattribute()
    {
        _attributeData = GameManage.Instance.currentAttribute;
        if(currentLevelText == null)
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
}
