using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Purchasing;


//
//数学库文档
//
public class No12_Math : MonoBehaviour
{
    private Vector3 startPosition;
    private float endtime = 9;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("度到弧度的换算常量"+Mathf.Deg2Rad);
        Debug.Log("弧度到度的换算常量"+Mathf.Rad2Deg);
        Debug.Log("正无穷大"+Mathf.Infinity);
        Debug.Log("负无穷大"+Mathf.NegativeInfinity);
        Debug.Log("兀"+Mathf.PI);//
        //静态函数
        Debug.Log("绝对值"+Mathf.Abs(-1.2f));//绝对值
        Debug.Log("小于等于的最大整数"+Mathf.Floor(2.74f));

        //Result = Mathf.Lerp(start,end, t);
        //result = start + （end - start）*t；
        //用法1
        //每帧改变start的值，变化速度先快后慢，位置无限接近，但是不会得到end的位置
        Debug.Log("a和b按照t进行线性插值"+Mathf.Lerp(1,10,Time.deltaTime));
        //用法2
        //每帧改变t的值，变化速度匀速，位置每帧接近，当t>=1时，得到结果
        float time = 0f;
        time += Time.deltaTime;
        Debug.Log("a和b按照t进行线性插值" + Mathf.Lerp(1, 10, time));

        //向下取整
        Debug.Log(Mathf.FloorToInt(9.6f));
        //向上取整
        float f = 1.3f;
        Debug.Log(Mathf.CeilToInt(f));
        //四舍五入
        Debug.Log(Mathf.RoundToInt(f));

        //钳制函数 (a,b,c)
        Debug.Log(Mathf.Clamp(10,11,20));//11  a<b<c  ->b 
        Debug.Log(Mathf.Clamp(21,11,20));//20  b<c<a  ->c
        Debug.Log(Mathf.Clamp(15,11,20));//15  b<a<c  ->a

        //返回一个数的平方根
        Debug.Log(Mathf.Sqrt(4));//2

        //判断一个数是不是2的次方
        Debug.Log(Mathf.IsPowerOfTwo(4));

        //一个数的n次幂
        Debug.Log(Mathf.Pow(4,2));//16

        //弧度角度转化
        //弧度转角度
        float rad = 1;
        float anger = rad * Mathf.Rad2Deg;
        //角度转弧度
        anger = 1;
        rad = rad * Mathf.Deg2Rad;
        //三角函数
        //mathf中的三角函数相关函数，传入的参数需要弧度值
        Debug.Log(Mathf.Sin(30*Mathf.Deg2Rad));//30度
        Debug.Log(Mathf.Cos(60*Mathf.Deg2Rad));//30度
        //反三角函数
        //得到的结果是 正弦或余弦值对应的弧度
        rad = Mathf.Asin(0.5f);
        Debug.Log(rad*Mathf.Rad2Deg);//30
        rad = Mathf.Acos(0.5f);
        Debug.Log(rad*Mathf.Rad2Deg);//60

        //正弦波的标准公式是：
        //y = A * sin(2π * f * t + φ)
        //A 是振幅（Amplitude）——波峰的高度。
        //f 是频率（Frequency）——每秒振动的次数（Hz）。
        //t 是时间。
        //φ 是相位（Phase）——初始时刻的偏移角度。
        //2π 是正弦函数的周期系数，因为正弦函数的自然周期是 2π，乘以它后，频率 f 就表示每秒完成 f 个完整波形。
    }



    // Update is called once per frame
    void Update()
    {
        //游戏倒计时
        Debug.Log(endtime);
        endtime = Mathf.MoveTowards(endtime, 0, 0.1f);
         //直线运动的距离
        float linDistence = 2 * Time.time;
        float sineOffset = 2 * Mathf.Sin((Time.time)* 2f*1* Mathf.PI);
        Vector3 newPos = startPosition + Vector3.right * linDistence;//横向
        newPos.y = startPosition.y + sineOffset;

        transform.position = newPos;
    }
}
