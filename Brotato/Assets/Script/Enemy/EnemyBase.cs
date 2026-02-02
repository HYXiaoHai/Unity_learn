using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public EnemyData _enemyData;

    public float attackTimer = 0f;//攻击定时器
    public bool isContact = false;//是否接触到玩家
    public bool isCooling = false;//是否是攻击冷却

    public float skillTimer = 0f;
    public bool isSkillCooling = false;//技能是否进入冷却
    public bool isDead = false;
    
    public bool elite = false;
    public GameObject money_prefab;//金币预制体
    public GameObject bullet_prefab;//子弹
    public GameObject circlebullet_prefab;//boss子弹

    public Rigidbody2D rb;
    public float knockbackDuration = 0.1f; // 击退持续时间
    private bool isKnockback = false; // 是否正在被击退
    private float knockbackTimer = 0f;
    private Vector2 knockbackDirection; // 击退方向

    [Header("音效设置")]
    public AudioClip hitSound;         // 受击音效
    public AudioClip deathSound;       // 死亡音效
    protected AudioSource audioSource; // 音频源

    public void Awake()
    {
        money_prefab = Resources.Load<GameObject>("Prefabs/Money");
        bullet_prefab = Resources.Load<GameObject>("Prefabs/Enemy_bullet");
        circlebullet_prefab = Resources.Load<GameObject>("Prefabs/Circle_bullet");
        hitSound = Resources.Load<AudioClip>("Music/攻击音效");
        deathSound = Resources.Load<AudioClip>("Music/受伤音效");
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
    }
    public virtual void Update()
    {
        if (Player.instance.isDead||isDead)
        { return; }

            //移动
            Move();

            //攻击
            if (isContact && !isCooling && !isDead)
            {
                Attack();
            }

        //更新计时器
        if (isCooling&&attackTimer>0)
        {
            attackTimer -= Time.deltaTime;
            if(attackTimer<=0)
            {
                attackTimer = 0;
                isCooling = false;
            }
        }
        // 更新击退计时器
        if (isKnockback && knockbackTimer > 0)
        {
            knockbackTimer -= Time.deltaTime;
            if (knockbackTimer <= 0)
            {
                EndKnockback();
            }
        }

        //更新技能计时器
        if ((isSkillCooling && skillTimer > 0))
        {
            skillTimer -= Time.deltaTime;
            if(skillTimer <= 0)
            {
                skillTimer = 0;
                isSkillCooling = false;
            }
        }
    }
    public void InitEnemy(EnemyData enemyData)
    {
        _enemyData = enemyData;
        Debug.Log("enemy_hp:"+enemyData.hp);
        Debug.Log("enemy_hp:"+_enemyData.hp);
        //避免一上来就放技能
        skillTimer = _enemyData.skillTime;
        isSkillCooling = true;
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
    public virtual void Move()
    {
        Vector2 direction = (Player.instance.transform.position - transform.position).normalized;//玩家位置 -  怪物位置 = 方向线路 (单位化)
        knockbackDirection = (direction*-1).normalized;
        transform.Translate(direction * _enemyData.speed * Time.deltaTime);
        TurnAround();
    }


    //自动转向
    public void TurnAround()
    {
        SpriteRenderer s = GetComponent<SpriteRenderer>();
        if (Player.instance.transform.position.x - transform.position.x >= 0.1f)//玩家在怪物右边
        {
            s.flipX = false;
            //transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if(Player.instance.transform.position.x - transform.position.x <= 0.1)//玩家在怪物右边
        {
            s.flipX = true;
            //transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    //普通攻击
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
    public virtual void UseSkill()
    {
        
    }
    //受伤
    public void Injured(float attack, float force = 0f)
    {
        if (isDead)
        {
            return;
        }
        //判断本次攻击是否会死亡
        if (_enemyData.hp - attack <= 0)
        {
            _enemyData.hp = 0;
            Dead();
        }
        else
        {
            _enemyData.hp -= attack;
            audioSource.PlayOneShot(hitSound);
            // 触发击退效果
            ApplyKnockback(force);
        }
    }
    // 应用击退效果
    public void ApplyKnockback(float force)
    {
        // 停止当前的物理速度
        rb.velocity = Vector2.zero;

        rb.AddForce(knockbackDirection * force, ForceMode2D.Impulse);

        // 设置击退状态
        isKnockback = true;
        knockbackTimer = knockbackDuration;

        // 可以在这里添加被击中的视觉效果
        StartCoroutine(FlashOnHit());
    }

    // 结束击退
    private void EndKnockback()
    {
        isKnockback = false;
        knockbackTimer = 0f;

        rb.velocity = Vector2.zero;
    }

    // 被击中时的闪烁效果
    private IEnumerator FlashOnHit()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
            yield break;

        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        spriteRenderer.color = originalColor;
    }

    //死亡
    public void Dead()
    {
        AudioSource.PlayClipAtPoint(deathSound, transform.position);
        //增加玩家经验值
        Player.instance.ExpUP(_enemyData.provideExp);


        //掉落金币
        Instantiate(money_prefab, transform.position, Quaternion.identity);
        
        //销毁
        Destroy(gameObject);
    }
    
}
