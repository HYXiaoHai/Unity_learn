using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PropsSelectPanel : MonoBehaviour
{
    public  static PropsSelectPanel instance; 

    public int haveMoneycount;//总共有的金币数目
    public TMP_Text _haveMoney;
    public TMP_Text _title;//标题 “商店(第2波)”
    public Button _nextWave;//下一关
    public TMP_Text _nextWaveText;//波数“出发(第*波)”
    public Button _refresh;//刷新

    public List<PropData> propDatas = new List<PropData>();//道具data信息
    public TextAsset PropsTextAsset;//json
    public GameObject prop_fabs;

    public List<GameObject> currentProps = new List<GameObject>();//当前展示的道具
    public List<PropsDetailsUI> slecetProps = new List<PropsDetailsUI>();//以选择的道具

    public WeaponUI weapon;
    public Transform _weaponList;
    public Transform _propsContent;

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
        //json
        PropsTextAsset = Resources.Load<TextAsset>("Data/prop");//读取
        propDatas = JsonConvert.DeserializeObject<List<PropData>>(PropsTextAsset.text);
        prop_fabs = Resources.Load<GameObject>("Prefabs/PropsDetail");
        //
        _propsContent = GameObject.Find("PropsContent").GetComponent<Transform>();
        _weaponList = GameObject.Find("WeaponList").GetComponent<Transform>();

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

        RenewProp();
        RenewMoney();
        _refresh.onClick.AddListener(() =>
          Refresh()
        );
        _nextWave.onClick.AddListener(() =>
          Nextwave()
        );
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
    //刷新面板
    public void RenewProp()
    {
        ClearCurrentProps();
        // 先生成4个随机索引（允许重复）
        int[] randomIndices = new int[4];
        for (int i = 0; i < 4; i++)
        {
            randomIndices[i] = Random.Range(0, propDatas.Count);
        }
        // 创建一个临时数组存储最终要显示的道具
        PropData[] finalProps = new PropData[4];
        // 先复制随机道具到数组
        for (int i = 0; i < 4; i++)
        {
            finalProps[i] = propDatas[randomIndices[i]];
        }
        // 用锁定的道具覆盖对应位置
        // 按照锁定列表中的顺序，覆盖前N个位置（N为锁定道具数量）
        for (int i = 0; i < GameManage.Instance.lockedPropIds.Count && i < 4; i++)
        {
            int lockedId = GameManage.Instance.lockedPropIds[i];
            // 找到锁定的道具数据
            PropData lockedProp = propDatas.Find(p => p.id == lockedId);
            if (lockedProp != null)
            {
                finalProps[i] = lockedProp;
            }
        }
        // 实例化道具面板
        for (int i = 0; i < 4; i++)
        {
            PropsDetailsUI p = Instantiate(prop_fabs, _propsContent).GetComponent<PropsDetailsUI>();
            p.Init(finalProps[i]);
            if (i < GameManage.Instance.lockedPropIds.Count)
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

    //下一关
    void Nextwave()
    {
        //重置关卡数据
        GameManage.Instance.NextWave();
        //开始下一关
        SceneManager.LoadScene(2);
    }
}
