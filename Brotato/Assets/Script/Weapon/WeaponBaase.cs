using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponBaase : MonoBehaviour
{
    public WeaponData data;//武器基本属性
    public bool isAttack = false;//是否可以攻击 必须在武器的范围内检测到有敌人。
    public bool isCooling = false;//是否正在冷却
    public bool canAiming = true;
    public float AttackTimer = 0;//攻击计时器
    public Transform enemy;// 要攻击的敌人
    public float originz;

    public AudioSource audioSource;

    public virtual void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        originz = transform.eulerAngles.z;
    }
    public  void Update()
    {
        if(Player.instance.isDead)
            { return; }

        //自动瞄准
        if (canAiming)
        Aiming();

        //判断是否可以攻击
        if (isAttack && !isCooling)
        {
            Fire();
        }
        //攻击的冷却
        if(isCooling)
        {
            AttackTimer += Time.deltaTime;
            if(AttackTimer >= data.cooling)
            {
                AttackTimer = 0;
                isCooling = false;
            }

        }
        
    }
    public void Aiming()
    {
        Collider2D[] enemysInRange = Physics2D.OverlapCircleAll(transform.position,data.range,LayerMask.GetMask("Enemy"));

        //
        if(enemysInRange.Length>0)
        {
            isAttack = true;
            Collider2D neatestEnemy = enemysInRange.OrderBy(enemy => Vector2.Distance(transform.position, enemy.transform.position)).First();

            enemy = neatestEnemy.transform;
            Vector2 enemyPos = (Vector2)enemy.position;

            Vector2 direction = enemyPos - (Vector2)transform.position;
            float angleDegrees = Mathf.Atan2(direction.y,direction.x)*Mathf.Rad2Deg;//弧度转角度
            //Debug.Log(angleDegrees);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y,angleDegrees+originz);
        }
        else
        {
            isAttack = false;
            enemy = null;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y,originz);
        }
    }

    public virtual void Fire()
    {

    }

}

