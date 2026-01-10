using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;
using UnityEngine.UIElements;

//
//用于表示2D向量
//
public class No7_Vector2 : MonoBehaviour
{
    public Transform gripsTrans;
    public Transform targetransform;
    public float percent;
    public float larpspeed;
    void Start()
    {
        ////静态边
        //print(Vector2.down);
        //print(Vector2.up);
        //print(Vector2.right);
        //print(Vector2.left);
        //print(Vector2.one);
        //print(Vector2.zero);
        ////构造函数
        //Vector2 v2 = new Vector2(2, 2);
        ////成员变量
        //Debug.Log("v2模长" + v2.magnitude);
        //Debug.Log("v2的模长的平方是" + v2.sqrMagnitude);
        //Debug.Log("v2的模长的平方是" + v2.sqrMagnitude);
        //Debug.Log("v2的单位化" + v2.normalized);//长度为1
        //Debug.Log(v2.x + "," + v2.y);//长度为1
        //Debug.Log(v2[0] + "," + v2[1]);//长度为1
        ////公共函数
        //bool qual = v2.Equals(new Vector2(1, 1));
        //Debug.Log(qual);
        //qual = (v2 == new Vector2(2, 2));
        //Debug.Log(qual);
        //v2 = new Vector2(1, 3);
        //Debug.Log("v2的单位化向量：" + v2.normalized + "但是v2的向量还是：" + v2);
        //v2.Normalize();
        //Debug.Log(v2);
        //v2.Set(3, 4);
        //Debug.Log(v2);
        ////test.position = v2;

        ////修改位置
        //transform.position = new Vector2(3, 3);
        //Vector2 vector2 = transform.position;
        //vector2.x = 2;
        //transform.position = vector2;

        ////静态函数
        //Vector2 va = new Vector2(1, 0);
        //Vector2 vb = new Vector2(0, 1);
        //Debug.Log(Vector2.Angle(va, vb));//va指向vb的方向计算无符号夹角
        //Debug.Log("va与vb的距离是"+Vector2.Distance(va, vb));
    
        //Debug.Log("vavb在各个方向上的最大分量组成的新向量是"+Vector2.Max(va, vb));
        //Debug.Log("vavb在各个方向上的最小分量组成的新向量是"+Vector2.Min(va, vb));
        
        ////具体得到的新向量的结果的计算公式：a+(b-a)*t
        //Debug.Log("va向vb按照0.5的比例进行线性插值变化的效果"+Vector2.Lerp(va,vb,0.5f));
        //Debug.Log("va向vb按照参数为-1的形式进行线性插值变化的结果是"+Vector2.LerpUnclamped(va,vb,0.5f));

        //float maxDistancd = 0.5f;
        ////将va以最大距离不超过maxDistance为移动步频移向vb；
        //Vector2.MoveTowards(va, vb, maxDistancd);//类似Lerp
        //Debug.Log("从va和vb之间有符号(度为单位，逆时针为正值)角度："+Vector2.SignedAngle(va,vb));
        //Debug.Log("从va和vb之间有符号(度为单位，逆时针为正值)角度："+Vector2.SignedAngle(vb,va));

        //Vector2 currentVelocity = new Vector2(1, 0);
        //Debug.Log("va，vb向量转换"+Vector2.SmoothDamp(va,vb,ref currentVelocity,0.1f));//平滑阻尼

        ////运算符
        //Debug.Log(va + vb);
        //Debug.Log(va - vb);
        //Debug.Log(va * 10);
        //Debug.Log(va == vb);

        
    }

    // Update is called once per frame
    void Update()
    {
        percent += 1 * larpspeed;
        gripsTrans.position = Vector2.Lerp(gripsTrans.position, targetransform.position, percent);

    }
}
