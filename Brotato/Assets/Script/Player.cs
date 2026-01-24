using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public static Player instance;
    public RoleData _roleData;
    public float speed = 5f;//速度
    public Transform playerVisual;//玩家实体
    public Transform turn;//玩家转向

    public float hp = 15f;
    public float maxHp = 15f;//当前最大生命值
    public bool isDead = false;
    public int money = 30;//当前金币
    public float exp = 0f;//经验值
    public float maxExp = 12f;//升级所需的经验值

    public Animator anim;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
        playerVisual = GameObject.Find("PlayerVisual").GetComponent<Transform>();
        turn = GameObject.Find("Turn").GetComponent<Transform>();
        anim = playerVisual.GetComponent<Animator>();
        //_roleData = GameManage.Instance.currentRole;
    }
    //设置角色
    private void Start()
    {
        playerVisual.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(_roleData.avatar);
    }

    void Update()
    {
        if(isDead)
        { return; }
        Move();
    }

    //wasd移动

    public void Move()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");//水平方向的动量（-1 0 1）
        float moveVertical = Input.GetAxisRaw("Vertical");//水平方向的动量（-1 0 1）
        //UnityEngine.Debug.Log(moveHorizontal);
        Vector2 moveMent = new Vector2(moveHorizontal, moveVertical);

        moveMent.Normalize();
        transform.Translate(moveMent * speed * Time.deltaTime);

        if (moveMent.magnitude != 0)
        {
            anim.SetBool("isMove", true);
        }
        else
        {
            anim.SetBool("isMove", false);
        }
        TurnAround(moveHorizontal);
    }
    //转头
    public void TurnAround(float h)
    {
        if (h == -1)
        {
            turn.localScale = new Vector3(-Mathf.Abs(turn.localScale.x), playerVisual.localScale.y, playerVisual.localScale.z);
        }
        else if (h == 1)
        {
            turn.localScale = new Vector3(Mathf.Abs(turn.localScale.x), playerVisual.localScale.y, playerVisual.localScale.z);
        }
    }

    //受伤
    public void Injured(float attack)
    {
        if (isDead)
        {
            return;
        }
        //判断本次攻击是否会死亡
        if (hp - attack <= 0)
        {
            hp = 0;
            Dead();
        }
        else
        {
            hp -= attack;
            UnityEngine.Debug.Log("玩家收到攻击 当前hp" + hp);
        }

        //更新血条
        GamePanel.instance.RenewHp();
    }

    //死亡
    public void Dead()
    {
        isDead = true;

        anim.speed = 0;
        //游戏失败函数
        LevelController.Instance.BadGame();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Money"))
        {
            Destroy(collision.gameObject);

            money ++;
            GamePanel.instance.RenewMoney(); 
        }
    }
}
