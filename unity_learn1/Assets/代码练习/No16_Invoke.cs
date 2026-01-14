using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
//延时调用
//
//也是协程的一种，但是调用的函数不能带参数
public class No16_Invoke : MonoBehaviour
{
    public GameObject gris;
    void Start()
    {
        //延迟三秒
        Invoke("CreatGris",3);
        //循环生成(函数名，第一次延迟的时间，以后循环调用间隔的时间)
        InvokeRepeating("CreatGris",1,3);

        //停止
        CancelInvoke("CreatGris");
        CancelInvoke();//停止所有
    }

    void Update()
    {
        //是否调用该函数
        Debug.Log(IsInvoking("CreatGris"));
        Debug.Log(IsInvoking());//有Invoke函数调用就传true。
    }
    private void CreatGris()
    {
        Instantiate(gris);
    }
}
