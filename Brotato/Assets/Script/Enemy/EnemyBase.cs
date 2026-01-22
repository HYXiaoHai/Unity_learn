using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float hp;//血量
    public float damage;//攻击力
    public float speed =3;//移动速度
    public float attackTime;//攻击间隔
    public float attackTimer = 0f;//攻击定时器
    public bool isContact = false;//是否接触到玩家
    public bool isCooling = false;//是否是攻击冷却
    public int provideExp = 1;//掉落经验值

    public GameObject money_prefab;//金币预制体

    private void Awake()
    {
        money_prefab = Resources.Load<GameObject>("Prefabs/Money");
    }
    void Start()
    {
        
    }

    void Update()
    {

        Move();
    }

    //自动移动
    public void Move()
    {
        Vector2 direction = (Player.instance.transform.position - transform.position).normalized;//玩家位置 -  怪物位置 = 方向线路 (单位化)
        transform.Translate(direction * speed * Time.deltaTime);
        TurnAround();
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

    //受伤

    //死亡


    ///////////////////////////////////////////
    
}
