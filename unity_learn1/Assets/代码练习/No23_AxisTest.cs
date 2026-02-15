using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class No23_AxisTest : MonoBehaviour
{
    private void Start()
    {
        //开启多点触摸
        Input.multiTouchEnabled = true;
    }

    private void Update()
    {
        #region 虚拟轴
        //获得水平轴
        float honizontal = Input.GetAxis("Horizontal");
        float vecrtical = Input.GetAxis("Vertical");
        
        //获得虚拟按键
        if(Input.GetButtonDown("Jump"))
        {

        }
        #endregion

        #region 触摸屏
        //判断单点触摸
        if (Input.touchCount == 1)
        {
            //触摸对象
            Touch touc = Input.touches[0];
            //触摸位置
            Debug.Log(touc.position);
            //触摸阶段
            switch(touc.phase)
            {
                case TouchPhase.Began:break;
                case TouchPhase.Moved:break;
                case TouchPhase.Stationary:break;
                case TouchPhase.Ended:break;
                case TouchPhase.Canceled:break;
            }
        }

        //多点触摸
        if(Input.touchCount == 2)
        {
            //获得触摸对象
            Touch touch1 = Input.touches[0];
            Touch touch2 = Input.touches[1];
        }
        #endregion
    }
}
