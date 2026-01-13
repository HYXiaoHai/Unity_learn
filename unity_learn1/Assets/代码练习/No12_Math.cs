using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//
//数学库文档
//
public class No12_Math : MonoBehaviour
{
    private float endtime = 9;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("度到弧度的换算常量"+Mathf.Deg2Rad);
        Debug.Log("弧度到度的换算常量"+Mathf.Rad2Deg);
        Debug.Log("正无穷大"+Mathf.Infinity);
        Debug.Log("负无穷大"+Mathf.NegativeInfinity);
        Debug.Log("兀"+Mathf.PI);
        //静态函数
        Debug.Log("绝对值"+Mathf.Abs(-1.2f));
        Debug.Log("小于等于的最大整数"+Mathf.Floor(2.74f));
        Debug.Log("a和b按照t进行线性插值"+Mathf.Lerp(1,2,0.5f));

    }

    // Update is called once per frame
    void Update()
    {
        //游戏倒计时
        Debug.Log(endtime);
        endtime = Mathf.MoveTowards(endtime, 0, 0.1f);
    }
}
