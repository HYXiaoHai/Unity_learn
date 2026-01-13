using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class No9_MassageTest : MonoBehaviour
{
    public void GetMsg()
    {
        Debug.Log("测试对象身上的消息");
    }
    public void GetSrcMsg(string str)
    {
        Debug.Log("测试对象身上的消息GetSrcMsg" + str);
    }
}
