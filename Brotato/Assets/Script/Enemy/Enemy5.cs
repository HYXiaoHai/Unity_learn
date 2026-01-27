using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy5 : EnemyBase
{
    [Header("冲锋技能")]
    public float chargeSpeedMultiplier = 1f;//冲锋倍数
    public float chargeDistance = 6f;//冲刺距离
    public float chargeTime = 0.5f;//冲刺时间

    private bool isCharging = false;//是否冲刺 
    private Vector2 chargingStartPosition;//冲刺开始位置
    private Vector2 chargingTargetPosition;//冲刺目标位置

    [Header("子弹技能")]
    public float bulletTimer = 1f;//当前时间
    public bool isbulletCooling = true;
    public float continuouTime = 10f;//持续时间
    public float Radius = 2f;//初始半径
    public int bulletNum = 9;//子弹数量
    public int bluck = 3;//每三个空一个
    public float bulletDistance = 1f;//子弹间隔
    public float bulletRotationSpeed = 90f;  // 添加旋转速度变量
    public bool isActive = false;// 是否激活
    private List<CircleBullet>bullets = new List<CircleBullet>();
    public override void Update()
    {
        base.Update();
        if (!isbulletCooling && !isActive)
        {
            StartCoroutine(StartBullet());
        }
        // 更新子弹计时器
        if (bulletTimer>0&&!isActive)
        {
            bulletTimer -= Time.deltaTime;
            if(bulletTimer<=0)
            {
                bulletTimer = 0;
                isbulletCooling = false;
            }
        }
    }
    public override void Move()
    {
        //冲刺
        if (!isSkillCooling && CheckDistence() && !isCharging)
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
        if (!isSkillCooling && !isCharging)
        {
            StartCoroutine(StartCharge());
        }

    }

    //检擦冲锋距离
    bool CheckDistence()
    {
        if (Vector2.Distance(Player.instance.transform.position, transform.position) <= _enemyData.range)
        {
            return true;
        }
        return false;
    }

    // 旋转子弹
    public IEnumerator StartBullet()
    {
        if (circlebullet_prefab == null)
        {
            Debug.LogError("circlebullet_prefab 未设置！");
            yield break;
        }
        isActive = true;

        // 计算初始方向（指向玩家的反方向）
        Vector3 dir = Vector3.right; //默认方向
        if (Player.instance != null)
        {
            dir = ((Player.instance.transform.position - transform.position) * -1).normalized;
        }

        float radius = Radius; //初始半径

        for (int i = 0; i < bulletNum+(bulletNum/bluck); i++)
        {
            if(i% (bluck+1)!= 0)//每三个空一格
            {
                // 实例化子弹
                GameObject bulletObj = Instantiate(
                    circlebullet_prefab,
                    transform.position, // 世界位置在圆心
                    Quaternion.identity
                );

                // 设置为子物体
                bulletObj.transform.SetParent(transform);
                bulletObj.transform.localPosition = Vector3.zero; // 先放在圆心

                CircleBullet bullet = bulletObj.GetComponent<CircleBullet>();
                if (bullet != null)
                {
                    // 初始化子弹
                    bullet.Init(radius, dir);
                    bullet.rotationSpeed = bulletRotationSpeed;

                    bullets.Add(bullet);

                    Debug.Log($"生成子弹 {i}: 半径={radius}, 方向={dir}");
                }
            }
            radius += bulletDistance;
        }

        // 等待持续时间
        yield return new WaitForSeconds(continuouTime);

        // 结束子弹效果
        EndBulletEffect();

        // 进入冷却
        bulletTimer = _enemyData.skillTime;
        isbulletCooling = true;
        isActive = false;
    }
    //结束子弹
    void EndBulletEffect()
    {
        foreach (var bullet in bullets)
        {
            if (bullet != null)
                Destroy(bullet.gameObject);
        }

        bullets.Clear();
        isActive = false;
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
        while (elapsedtime < chargeTime)
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
        isCharging = false;
        //进入冷却
        isSkillCooling = true;
        skillTimer = _enemyData.skillTime;
    }


}
