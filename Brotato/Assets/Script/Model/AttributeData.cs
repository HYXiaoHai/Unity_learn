using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AttributeData
{
    // 基础属性
    public int currentLevel;                // 当前等级
    // 生命相关
    public float maxHealth;                 // 最大生命值
    public float healthRegeneration;        // 生命再生（每秒恢复量）
    public float lifeStealPercent;          // %生命窃取（0-1，表示百分比）
    // 伤害相关
    public float meleeDamage;               // 近战伤害
    public float damagePercent;             // %伤害
    public float rangedDamage;              // 远程伤害
    public float elementalDamage;           // 元素伤害
    public float criticalRate;              // 暴击率
    // 工程学相关
    public float engineering;               // 工程学（可能是召唤物伤害或数量）
    // 战斗效率
    public float attackSpeedPercent;        // %攻击速度（0-1）
    public float range;                     // 攻击范围
    // 防御相关
    public float armor;                     // 护甲值
    public float dodgePercent;              // %闪避率（0-1）
    public float speedPercent;              // %移动速度（0-1）
    public float luck;                      // 幸运值（可能影响掉落率）
    public float harvest;                   // 收获（采集效率）
}