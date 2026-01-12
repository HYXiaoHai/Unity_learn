using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//
//消息发送
//消耗很大 注意不要滥用
public class No9_Message : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //仅给自己发消息（以及身上其他的monoBegaviour对象）
        gameObject.SendMessage("GetMsg");
        SendMessage("GetSrcMsg", "66666");
        SendMessage("不存在函数", SendMessageOptions.DontRequireReceiver);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GetMsg()
    {
        Debug.Log("测试本身对象的消息");
    }
    public void GetSrcMsg(string str)
    {
        Debug.Log("测试本身的消息GetSrcMsg"+str);
    }

}
