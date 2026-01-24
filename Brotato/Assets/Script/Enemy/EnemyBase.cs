using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    //public float hp;//血量
    //public float damage;//攻击力
    //public float speed =3;//移动速度
    //public float attackTime;//攻击间隔
    //public int provideExp = 1;//掉落经验值

    public EnemyData _enemyData;

    public float attackTimer = 0f;//攻击定时器
    public bool isContact = false;//是否接触到玩家
    public bool isCooling = false;//是否是攻击冷却

    public GameObject money_prefab;//金币预制体

    public void Awake()
    {
        money_prefab = Resources.Load<GameObject>("Prefabs/Money");
    }
    public void Update()
    {
        if (Player.instance.isDead)
        { return; }
        //移动
        Move();
        
        //攻击
        if(isContact && !isCooling)
        {
            Attack();
        }

        //更新计时器
        if(isCooling&&attackTimer>0)
        {
            attackTimer -= Time.deltaTime;
            if(attackTimer<=0)
            {
                attackTimer = 0;
                isCooling = false;
            }
        }
    }
    public void InitEnemy(EnemyData enemyData)
    {
        _enemyData = enemyData;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            isContact = true;
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            isContact = false;
        }
    }

    //自动移动
    public void Move()
    {
        Vector2 direction = (Player.instance.transform.position - transform.position).normalized;//玩家位置 -  怪物位置 = 方向线路 (单位化)
        transform.Translate(direction * _enemyData.speed * Time.deltaTime);
        //TurnAround();
    }


    //自动转向
    public void TurnAround()
    {
        if(Player.instance.transform.position.x - transform.position.x >= 0.1f)//玩家在怪物右边
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if(Player.instance.transform.position.x - transform.position.x <= 0.1)//玩家在怪物右边
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    //攻击
    public void Attack()
    {
        if(isCooling)
        {
            return;
        }
        Player.instance.Injured(_enemyData.damage);
     
        //攻击进入冷却
        isCooling = true;
        attackTimer = _enemyData.attackTime;

    }

    //受伤
    public void Injured(float attack)
    {
        //if (isDead)
        //{
        //    return;
        //}
        //判断本次攻击是否会死亡
        if (_enemyData.hp - attack <= 0)
        {
            _enemyData.hp = 0;
            Dead();
        }
        else
        {
            _enemyData.hp -= attack;
        }
    }

    //死亡
    public void Dead()
    {
        //增加玩家经验值
        Player.instance.exp += _enemyData.provideExp;
        GamePanel.instance.RenewExp();

        //掉落金币
        Instantiate(money_prefab, transform.position, Quaternion.identity);
        
        //销毁
        Destroy(gameObject);
    }
    ///////////////////////////////////////////
    
}
