using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

//锁定道具
public struct ShopLock
{
    public bool isweapon; // true：武器 false：道具
    public int id; // id

    // 实现相等比较
    public override bool Equals(object obj)
    {
        if (!(obj is ShopLock)) return false;
        ShopLock other = (ShopLock)obj;
        return isweapon == other.isweapon && id == other.id;
    }

    public override int GetHashCode()
    {
        return (isweapon ? 1 : 0) * 1000 + id;
    }

    public static bool operator ==(ShopLock a, ShopLock b)
    {
        return a.isweapon == b.isweapon && a.id == b.id;
    }

    public static bool operator !=(ShopLock a, ShopLock b)
    {
        return !(a == b);
    }
}
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
    public List<PropData> currentProp = new List<PropData>();//已购买的道具
    public int currentMoney = 30;//当前的金币数
    //public List<int> lockedPropIds = new List<int>(); //存储锁定的道具ID列表
    public List<ShopLock> lockedPropIds = new List<ShopLock>(); //存储锁定的道具ID列表
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

    // 锁定相关方法
    public bool TryAddLock(bool isWeapon, int id)
    {
        if (lockedPropIds.Count >= MAX_LOCKS)
            return false;

        ShopLock newLock = new ShopLock { isweapon = isWeapon, id = id };

        lockedPropIds.Add(newLock);
        return true;
    }
    public bool TryRemoveLock(bool isWeapon, int id)
    {
        ShopLock lockToRemove = new ShopLock { isweapon = isWeapon, id = id };
        return lockedPropIds.Remove(lockToRemove);
    }

    public bool IsLocked(bool isWeapon, int id)
    {
        ShopLock checkLock = new ShopLock { isweapon = isWeapon, id = id };
        return lockedPropIds.Contains(checkLock);
    }

    // 获取武器锁定ID数组
    public int[] ReturnWeaponID()
    {
        List<int> weaponIds = new List<int>();
        foreach (var lockItem in lockedPropIds)
        {
            if (lockItem.isweapon)
            {
                weaponIds.Add(lockItem.id);
            }
        }
        return weaponIds.ToArray();
    }

    // 获取道具锁定ID数组
    public int[] ReturnPropID()
    {
        List<int> propIds = new List<int>();
        foreach (var lockItem in lockedPropIds)
        {
            if (!lockItem.isweapon)
            {
                propIds.Add(lockItem.id);
            }
        }
        return propIds.ToArray();
    }

    // 清空所有锁定
    public void ClearAllLocks()
    {
        lockedPropIds.Clear();
    }
    //////////////////////////////////////////////////////////////////////
    /////////////////////////////武器合成/////////////////////////////////////
    // 检查是否可以合成指定武器（手动合成检查）
    public bool CanMergeWeapon(int weaponId, int grade)
    {
        int count = 0;
        foreach (var weapon in currentWeapon)
        {
            if (weapon.id == weaponId && weapon.grade == grade)
            {
                count++;
                if (count >= 2) return true;
            }
        }
        return false;
    }

    // 检查是否可以自动合成（购买时检查）
    public bool CanAutoMergeWeapon(int weaponId, int grade)
    {
        foreach (var weapon in currentWeapon)
        {
            if (weapon.id == weaponId && weapon.grade == grade)
                return true;
        }
        return false;
    }

    // 手动合成武器（从背包中合成）
    public bool TryMergeWeapon(int weaponId, int grade)
    {
        if (!CanMergeWeapon(weaponId, grade))
            return false;

        // 找到两个要合成的武器
        List<WeaponData> weaponsToRemove = new List<WeaponData>();
        foreach (var weapon in currentWeapon)
        {
            if (weapon.id == weaponId && weapon.grade == grade && weaponsToRemove.Count < 2)
            {
                weaponsToRemove.Add(weapon);
            }
        }

        if (weaponsToRemove.Count < 2) return false;

        // 获取第一个武器作为升级基础
        WeaponData baseWeapon = weaponsToRemove[0];

        // 移除两个武器
        foreach (var weapon in weaponsToRemove)
        {
            currentWeapon.Remove(weapon);
        }

        // 创建升级后的武器
        WeaponData upgradedWeapon = CreateUpgradedWeapon(baseWeapon);

        // 添加到背包
        currentWeapon.Add(upgradedWeapon);

        return true;
    }

    // 自动合成武器（购买时合成）
    public bool TryAutoMergeWeapon(int weaponId, int grade, WeaponData purchasedWeapon)
    {
        if (!CanAutoMergeWeapon(weaponId, grade))
            return false;

        // 找到背包中要移除的武器
        WeaponData weaponToRemove = null;
        foreach (var weapon in currentWeapon)
        {
            if (weapon.id == weaponId && weapon.grade == grade)
            {
                weaponToRemove = weapon;
                break;
            }
        }

        if (weaponToRemove == null) return false;

        // 移除背包中的武器
        currentWeapon.Remove(weaponToRemove);

        // 创建升级后的武器（基于购买的武器）
        WeaponData upgradedWeapon = CreateUpgradedWeapon(purchasedWeapon);

        // 添加到背包
        currentWeapon.Add(upgradedWeapon);

        return true;
    }

    // 创建升级后的武器（核心方法）
    private WeaponData CreateUpgradedWeapon(WeaponData baseWeapon)
    {
        // 直接克隆并修改
        WeaponData upgraded = baseWeapon.Clone();

        // 修改属性
        upgraded.grade += 1;  // 等级+1
        upgraded.damage *= 1.5f;  // 伤害增加50%

        return upgraded;
    }
    /////////////////////////////////////////////////////////////////////////
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
