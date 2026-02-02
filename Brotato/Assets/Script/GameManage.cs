using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


//现在这个机制有点不对，
//1.锁定一个道具后，当再次出现该道具，会被自动判定已锁定
//2.他只能锁定不同样的道具，当锁定同一个道具的时候，他就不管用了（猜测是因为没有保存到数组里面）
public class GameManage : MonoBehaviour
{
    public static GameManage Instance;
    [Header("角色")]
    public RoleData currentRole;//当前角色
    public AttributeData currentAttribute;//当前角色属性
    public int slot;//插槽
    public float shopDiscount;//商店折扣
    public float expMuti;//经验增长值
                                         
    [Header("武器")]
    public List<WeaponData> currentWeapon = new List<WeaponData>();//记录当前所有武器
    [Header("道具")]
    public List<PropData> currentProp = new List<PropData>();//记录当前所有的道具
    public int currentMoney = 30;//当前的金币数
    public List<int> lockedPropIds = new List<int>(); //存储锁定的道具ID列表
    public const int MAX_LOCKS = 4; //最大锁定数量
    [Header("关卡信息")]
    public DifficutyData currentDifficulty;//当前难度
    public int currentWave = 1;//当前波数（1-6）
    [Header("敌人信息")]
    public List<EnemyData> enemyDatas = new List<EnemyData>();//
    public TextAsset enemyTextAsset;
    
    private void Awake()
    {
        // 完整的单例模式实现
        if (Instance == null)
        {
            Instance = this;
            UnityEngine.Debug.Log("gamemanage instance :" + Instance);
            DontDestroyOnLoad(gameObject);

        }
        else if (Instance != this)
        {
            // 如果已存在实例，销毁新创建的
            Destroy(gameObject);
            return;
        }

        enemyTextAsset = Resources.Load<TextAsset>("Data/enemy");
        if (enemyTextAsset != null)
        {
            enemyDatas = JsonConvert.DeserializeObject<List<EnemyData>>(enemyTextAsset.text);
            UnityEngine.Debug.Log($"加载了 {enemyDatas?.Count ?? 0} 个敌人数据");
        }
        else
        {
            UnityEngine.Debug.LogError("无法加载 enemy.json 文件");
        }

    }
    //崭新的游戏
    public void NewGame()
    {
        InitAttribute();//初始化属性。
        currentWave = 1;
        shopDiscount = 0;
        slot = 4;
        expMuti = 0;
        currentMoney = 30;
    }
    //前往下一波
    public void NextWave()
    {
        currentWave++;
    }
    public void Start()
    {

    }
    //角色属性
    private void InitAttribute()
    {
        currentAttribute.currentLevel = 1;
        currentAttribute.maxHealth = 15;
        currentAttribute.healthRegeneration = 30f;
        currentAttribute.lifeStealPercent = 30f;
        currentAttribute.range = 30f;
        currentAttribute.meleeDamage = 30;
        currentAttribute.damagePercent = 30;
        currentAttribute.meleeDamage = 30;
        currentAttribute.rangedDamage = 30;
        currentAttribute.rangedDamage = 30;
        currentAttribute.elementalDamage = 30;
        currentAttribute.criticalRate = 30;
        currentAttribute.engineering = 30;
        currentAttribute.attackSpeedPercent = 30;
        currentAttribute.armor = 30;
        currentAttribute.dodgePercent = 30;
        currentAttribute.speedPercent = 30;
        currentAttribute.luck = 30;
        currentAttribute.harvest = 30;
    }
    // 用于场景切换后确保单例可用
    public static void EnsureInstanceExists()
    {
        if (Instance == null)
        {
            // 在场景中查找现有的 GameManage
            GameManage existing = FindObjectOfType<GameManage>();
            if (existing != null)
            {
                Instance = existing;
            }
            else
            {
                // 如果不存在，创建一个新的
                GameObject go = new GameObject("GameManager");
                Instance = go.AddComponent<GameManage>();
                DontDestroyOnLoad(go);
            }
        }
    }

    /////////////////////////锁定道具相关方法///////////////////////////////////
    // 只保留清空方法（新游戏时用）
    public void ClearAllLocks()
    {
        lockedPropIds.Clear();
    }
    //////////////////////////////////////////////////////////////////////
    public object RandomOne<T>(List<T> list)
    {
        if (list == null || list.Count == 0)
        {
            return null;
        }
        int index = Random.Range(0, list.Count);
        return list[index];
    }
}
