using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class Enemy3 : EnemyBase
{
    public override void Move()
    {
        base.Move();

        if(!isSkillCooling&&CheckDistence())
        {
            UseSkill();
        }
    }

    bool CheckDistence()
    {
        if (Vector2.Distance(Player.instance.transform.position, transform.position) <= _enemyData.range)
        {
            return true;
        }
        return false;
    }

    public override void UseSkill()
    {
        Vector3 bulletDirection = (Player.instance.transform.position - transform.position).normalized;
        Debug.Log("bulletDirection£º"+ bulletDirection);
        EnemyBullet e = Instantiate(bullet_prefab,transform.position,Quaternion.identity).GetComponent<EnemyBullet>();
        e.Init(bulletDirection);
        Debug.Log("·¢Éä×Óµ¯");
        isSkillCooling = true;
        skillTimer = _enemyData.skillTime;
    }
}
