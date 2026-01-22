using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public static Player instance;

    public float speed = 5f;
    public Transform playerVisual;
    public Transform turn;

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
    }
    void Start()
    {
        
    }

    void Update()
    {
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
        transform.Translate(moveMent*speed*Time.deltaTime);

        if(moveMent.magnitude!=0)
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
        UnityEngine.Debug.Log(h);
        if (h == -1)
        {
            turn.localScale = new Vector3(-Mathf.Abs(turn.localScale.x), playerVisual.localScale.y, playerVisual.localScale.z);
        }
        else if(h == 1)
        {
            turn.localScale = new Vector3(Mathf.Abs(turn.localScale.x), playerVisual.localScale.y, playerVisual.localScale.z);
        }
    }

    //受伤
    public void Injured(float attack)
    {

    }

    //攻击
    public void attack()
    {

    }


    //死亡
    public void Dead()
    {

    }
}
