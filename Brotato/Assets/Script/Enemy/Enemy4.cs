using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy4 : EnemyBase
{
    [Header("冲锋技能")]
    public float chargeSpeedMultiplier = 1f;//冲锋倍数
    public float chargeDistance = 6f;//冲刺距离
    public float chargeTime = 0.5f;//冲刺时间

    private bool isCharging;//是否冲刺 
    private Vector2 chargingStartPosition;//冲刺开始位置
    private Vector2 chargingTargetPosition;//冲刺目标位置
    public override void Move()
    {
        //冲刺
        if(!isSkillCooling&&CheckDistence()&&!isCharging)
        {
            Debug.Log("开始冲刺");
            UseSkill();
        }
        else
        {
            if (isCharging)
                return;
            base.Move();
        }
    }
    public override void UseSkill()
    {
        if ( !isSkillCooling && !isCharging)
        {
            StartCoroutine(StartCharge());
        }
    }

    //检擦冲锋距离
    bool CheckDistence()
    {
        if(Vector2.Distance(Player.instance.transform.position,transform.position)<=_enemyData.range)
        {
            return true;
        }
        return false;
    }

    //开始冲锋
    IEnumerator StartCharge()
    {
        isCharging = true;
        // 记录开始位置
        chargingStartPosition = transform.position;
        // 计算朝向玩家的方向
        Vector2 directionToPlayer = ((Vector2)Player.instance.transform.position - chargingStartPosition).normalized;

        // 计算目标位置：当前位置 + 方向 * 距离
        chargingTargetPosition = chargingStartPosition + directionToPlayer * chargeDistance;

        float elapsedtime = 0f;
        while (elapsedtime<chargeTime)
        {
            //计算插值比例
            float t = elapsedtime / chargeTime;
            // 使用Lerp从起始位置移动到目标位置
            transform.position = Vector2.Lerp(chargingStartPosition, chargingTargetPosition, t);
            elapsedtime += Time.deltaTime;
            
            yield return null;
        }
        // 确保最终位置准确
        transform.position = chargingTargetPosition;
        isCharging =false;
        //进入冷却
        isSkillCooling = true;
        skillTimer = _enemyData.skillTime;
    }
}
