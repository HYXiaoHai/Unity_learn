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

    // 克隆方法 - 创建新的实例
    public EnemyData Clone()
    {
        return new EnemyData
        {
            id = this.id,
            name = this.name,
            hp = this.hp,          // 每次克隆时复制原始血量
            damage = this.damage,
            speed = this.speed,
            attackTime = this.attackTime,
            provideExp = this.provideExp,
            skillTime = this.skillTime,
            range = this.range
        };
    }
}
