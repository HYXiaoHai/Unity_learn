using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class No2_EventFunctions : MonoBehaviour
{
    public float attectvallu = 10;
    private void Reset()
    {
        Debug.Log("调用了Reset");
    }

    private void Awake()
    {
        Debug.Log("调用了Awake");
    }
    private void OnEnable()
    {
        Debug.Log("调用了OnEnable");

    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("调用了Start");

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("调用了Update");

    }
    private void LateUpdate()
    {
        Debug.Log("调用了LateUpdate");

    }
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
    private void OnDestroy()
    {
        //场景或游戏结束
        //停止播放模式
        //当脚本被移除
        //当前脚本对象被销毁
        Debug.Log("调用了OnDestroy");
    }
}
