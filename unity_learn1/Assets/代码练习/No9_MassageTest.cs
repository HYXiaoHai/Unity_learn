using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class No9_MassageTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GetMsg()
    {
        Debug.Log("测试对象身上的消息");
    }
    public void GetSrcMsg(string str)
    {
        Debug.Log("测试对象身上的消息GetSrcMsg" + str);
    }
}
