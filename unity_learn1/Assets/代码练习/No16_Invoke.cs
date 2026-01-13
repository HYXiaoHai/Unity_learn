using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
//延时调用
public class No16_Invoke : MonoBehaviour
{
    public GameObject gris;
    void Start()
    {
        //延迟三秒
        Invoke("CreatGris",3);
        //循环生成(函数名，第一次延迟的时间，以后循环调用间隔的时间)
        InvokeRepeating("CreatGris",1,3);


    }

    void Update()
    {
        
    }
    private void CreatGris()
    {
        Instantiate(gris);
    }
}
