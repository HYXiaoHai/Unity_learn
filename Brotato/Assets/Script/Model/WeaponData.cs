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
    public int grade;
    public int price;
    public float damage;
    public int isLong;//ÊÇ·ñÔ¶³Ì
    public int range;//¹¥»÷·¶Î§
    public float critical_strikes_multiple;//±©»÷±¶Êı
    public float critical_strikes_probability;//±©»÷ÂÊ
    public float cooling;//ÀäÈ´
    public int repel;//»÷ÍËĞ§¹û
    public string describe;//ÃèÊö 
} 