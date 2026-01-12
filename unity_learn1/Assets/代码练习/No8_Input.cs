using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//访问输入系统的接口类


public class No8_Input : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        //连续检测（移动）
        //Debug.Log("当前玩家输入的水平方向轴值是：" + Input.GetAxis("Horizontal"));
        //Debug.Log("当前玩家输入的垂直方向轴值是：" + Input.GetAxis("Vertical"));

        //Debug.Log("当前玩家输入的水平方向的边界轴值是：" + Input.GetAxisRaw("Horizontal"));//-1 0 1
        //Debug.Log("当前玩家输入的垂直方向边界轴值是：" + Input.GetAxisRaw("Vertical"));

        //鼠标滑动
        //第一人称视角
        //商品栏向左滑动
        Debug.Log("当前玩家鼠标水平移动增量是：" + Input.GetAxis("Mouse X"));
        Debug.Log("当前玩家鼠标垂直移动增量是：" + Input.GetAxis("Mouse Y"));

        //间隔检测(事件)
        //攻击
        //间隔检测
        if(Input.GetButtonDown("Fire1"))
        {
            Debug.Log("当前玩家使用武器1进行攻击");
        }
        if(Input.GetButtonUp("Fire1"))
        {
            Debug.Log("当前玩家使用武器1进行攻击");
        }
        //连续检测
        if(Input.GetButton("Fire1"))
        {
            Debug.Log("当前玩家使用武器1进行攻击");
        }

        //指定键盘按键检测
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("当前玩家按下A");
        }
        if (Input.GetKey(KeyCode.B))
        {
            Debug.Log("当前玩家按下B");
        }
        if (Input.anyKeyDown)
        {
            Debug.Log("按下任意键");
        }

        //鼠标按键
        if (Input.GetMouseButton(0))
        {
            Debug.Log("当前玩家按下左键");
        }
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("当前玩家按下左键");
        }

        if (Input.GetMouseButton(1))
        {
            Debug.Log("当前玩家按下右键");
        }
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("当前玩家按下右键");
        }

        if (Input.GetMouseButton(2))
        {
            Debug.Log("当前玩家按下中键");
        }
        if (Input.GetMouseButtonDown(2))
        {
            Debug.Log("当前玩家按下中键");
        }

       
    }
}
