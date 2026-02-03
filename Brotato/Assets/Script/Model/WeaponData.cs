using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeaponData
{
    public int id;
    public string name;
    public string avatar;
    public int grade;//µÈ¼¶
    public int price;//¼ÛÇ®
    public float damage;
    public int isLong;//ÊÇ·ñÔ¶³Ì
    public int range;//¹¥»÷·¶Î§
    public float critical_strikes_multiple;//±©»÷±¶Êý
    public float critical_strikes_probability;//±©»÷ÂÊ
    public float cooling;//ÀäÈ´
    public int repel;//»÷ÍËÐ§¹û
    public string describe;//ÃèÊö 

    public WeaponData Clone()
    {
        return new WeaponData
        {
            id = this.id,
            name = this.name,
            avatar = this.avatar,
            grade = this.grade,
            price = this.price,
            damage = this.damage,
            isLong = this.isLong,
            range = this.range,
            critical_strikes_multiple = this.critical_strikes_multiple,
            critical_strikes_probability = this.critical_strikes_probability,
            cooling = this.cooling,
            repel = this.repel,
            describe = this.describe,
        };
    }
} 