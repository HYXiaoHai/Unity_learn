using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameProgressData
{
    // 全局统计数据
    public int totalPlayCount = 0;
    public int totalWinCount = 0;

    // 角色统计数据
    [Serializable]
    public class CharacterStats
    {
        public int characterId;
        public string characterName;
        public int playCount = 0;
        public int winCount = 0;
        public int highestWave = 0;
        public int highestDifficultyId = 0;
    }

    public List<CharacterStats> characterStats = new List<CharacterStats>();

    // 已解锁的角色ID列表（从RoleData.unlock字段同步）
    public List<int> unlockedCharacters = new List<int>() { 1, 2, 3 };

    // 当前游戏进度（直接保存所有需要的Data）
    public bool hasCurrentGame = false;

    // 直接保存各种Data，而不是ID
    public RoleData currentRole;
    public AttributeData currentAttribute;
    public DifficutyData currentDifficulty;
    public List<WeaponData> currentWeapons = new List<WeaponData>();
    public List<PropData> currentProps = new List<PropData>();
    public List<ShopLock> lockedItems = new List<ShopLock>();

    // 游戏状态
    public int currentWave = 1;
    public int currentMoney = 30;
    public int slot = 4;
    public float shopDiscount = 0;
    public float expMuti = 0;

    // 存档时间
    public string saveTime;
}