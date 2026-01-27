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

    [Header("初始化武器")]
    public List<WeaponData> weaponDatas = new List<WeaponData>();
    public List<Transform> w;
    public Transform w1;
    public Transform w2;
    public Transform w3;
    public Transform w4;
    public Transform w5;
    public Transform w6;
    public GameObject weapon_prefab;
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
        weaponDatas = GameManage.Instance.currentWeapon;
        //_roleData = GameManage.Instance.currentRole;

        w1 = GameObject.Find("w1").GetComponent<Transform>();
        w2 = GameObject.Find("w2").GetComponent<Transform>();
        w3 = GameObject.Find("w3").GetComponent<Transform>();
        w4 = GameObject.Find("w4").GetComponent<Transform>();
        w5 = GameObject.Find("w5").GetComponent<Transform>();
        w6 = GameObject.Find("w6").GetComponent<Transform>();
    }
    //设置角色
    private void Start()
    {
        playerVisual.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Image/人物/全能");
        w.Add(w1);
        w.Add(w2);
        w.Add(w3);
        w.Add(w4);
        w.Add(w5);
        w.Add(w6);
        InitWeapon();
        //playerVisual.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(_roleData.avatar);
    }

    void InitWeapon()
    {
        if(weaponDatas.Count>6)//最多6把武器
        {
            UnityEngine.Debug.LogWarning("武器系统异常");
        }

       for(int i =0;i<weaponDatas.Count; i++)
        {
            if(weaponDatas[i] == null)
            {
                break;
            }
            string s = "Prefabs/" + weaponDatas[i].name;
            UnityEngine.Debug.Log(s);
            weapon_prefab = Resources.Load<GameObject>(s);
            WeaponBaase wp = Instantiate(weapon_prefab, w[i]).GetComponent<WeaponBaase>();
            wp.data = weaponDatas[i];
        }
    }

    void Update()
    {
        if(isDead&&!LevelController.Instance.isOver)
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
