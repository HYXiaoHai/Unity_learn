using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
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

    public float hp = 0f;
    public float maxHp = 0f;//当前最大生命值
    public bool isDead = false;
    //public int money = 30;//当前金币
    public float exp = 0f;//经验值
    public float maxExp = 12f;//升级所需的经验值
    public int currentLevel = 0;
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
    public Animator anim;//角色动画

    public GameObject _expUP;//升级提示ui
    public Animator expAni;//升级提示动画 
    public Transform _epxUp;//升级提示的
    public GameObject expUPimage_prafbs;
    public Stack<GameObject>expUPimagePrafbs = new Stack<GameObject>();
    public AttributeData attribute;//属性
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
        _roleData = GameManage.Instance.currentRole;

        _epxUp = GameObject.Find("EXPUP").GetComponent<Transform>();
        _expUP = GameObject.Find("升级提示");
        expUPimage_prafbs = Resources.Load<GameObject>("Prefabs/ExpUPImage");

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
        //playerVisual.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Image/人物/全能");
        w.Add(w1);
        w.Add(w2);
        w.Add(w3);
        w.Add(w4);
        w.Add(w5);
        w.Add(w6);
        InitWeapon();
        Initattribute();//设置属性
        playerVisual.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(_roleData.avatar);

        expAni = _expUP.GetComponent<Animator>();
        _expUP.SetActive(false);

       
    }
    //设置属性
    public void Initattribute()
    {
        UnityEngine.Debug.Log(hp);
        UnityEngine.Debug.Log(maxExp);
        attribute = GameManage.Instance.currentAttribute;
        speed = 5*(float)(1+attribute.speedPercent*0.01);
        maxHp = attribute.maxHealth;
        hp = maxHp;
        UnityEngine.Debug.Log(hp);
        UnityEngine.Debug.Log(maxExp);
        UnityEngine.Debug.Log("--------------------------------");
        UnityEngine.Debug.Log(attribute.maxHealth);
        UnityEngine.Debug.Log(GameManage.Instance.currentAttribute.maxHealth);
        //money = GameManage.Instance.currentMoney;
        currentLevel = attribute.currentLevel;
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


    //升级
    public void ExpUP(float Exp)
    {
        float upExp = Exp *(1+GameManage.Instance.expMuti);//计算实际增加值
        exp += upExp;
        if(exp>=maxExp)
        {
            //满足升级
            LevelController.Instance.expUpCount++;
            currentLevel++;
            GameManage.Instance.currentAttribute.currentLevel++;
            float f = exp % maxExp;//exp归零并加上多余的经验
            exp = f;
            expUPimagePrafbs.Push(Instantiate(expUPimage_prafbs, _epxUp));//生成升级ui
            //播放动画
            StartCoroutine(StartexpUp());
        }
        GamePanel.instance.RenewExp();
    }
    //升级动画
    IEnumerator StartexpUp()
    {
        _expUP.SetActive(true);
        yield return null;
        expAni.Play("expUP");//开始播放
        yield return new WaitForSeconds(1);
        _expUP.SetActive(false); 
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
    //获取金币
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Money"))
        {
            Destroy(collision.gameObject);
            GameManage.Instance.currentMoney++;
            //money ++;
            GamePanel.instance.RenewMoney(); 
        }
    }
}
