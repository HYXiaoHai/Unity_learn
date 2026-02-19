using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class No2_EventFunctions : MonoBehaviour
{
    public float attectvallu = 10;
    //默认函数
    private void Reset()
    {
        Debug.Log("调用了Reset");
    }
    //创建时调用
    private void Awake()
    {
        Debug.Log("调用了Awake");
    }
    //唤醒 激活时调用
    private void OnEnable()
    {
        Debug.Log("调用了OnEnable");

    }
    //初始化的
    void Start()
    {
        Debug.Log("调用了Start");

    }
    //每一帧调用 用于物理更新
    //与一下两个Upedate不同
    //ProjectSetting----Time可以修改帧间隔时间
    private void FixedUpdate()
    {
        Debug.Log("调用了FixedUpdate");
    }
    void Update()
    {
        Debug.Log("调用了Update");

    }
    //晚一帧
    private void LateUpdate()
    {
        Debug.Log("调用了LateUpdate");

    }
    //对象失活的时候
    private void OnDisable()
    {
        Debug.Log("调用了OnDisable");

    }
    private void OnApplicationQuit()
    {
        //在程序退出之前所有的游戏对象都会调用此函数
        //编译器终止播放的时候
        //网页试图关闭的时候
        Debug.Log("调用了OnApplicationQuit");
    }
    //销毁的时候
    private void OnDestroy()
    {
        //场景或游戏结束
        //停止播放模式
        //当脚本被移除
        //当前脚本对象被销毁
        Debug.Log("调用了OnDestroy");
    }
}
