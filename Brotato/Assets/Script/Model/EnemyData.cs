using System;
[Serializable]
public class EnemyData 
{
    public int id;  //id
    public string name;//名字
    public float hp;//血量
    public float damage;//伤害
    public float speed;//速度
    public float attackTime;//攻击间隔/
    public float provideExp;//掉落经验
    public float skillTime;//技能时间间隔（-1表示无技能）
    public float range;//攻击范围 （-1为近战贴身）
}
