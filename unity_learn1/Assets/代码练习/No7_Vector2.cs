using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

//
//用于表示2D向量
//
public class No7_Vector2 : MonoBehaviour
{
    public GameObject test;
    public float moveSpeed = 5f; // 控制移动速度
    // Start is called before the first frame update
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
        //Vector2 v2 = new Vector2(2,2);
        ////成员变量
        //Debug.Log("v2模长" + v2.magnitude);
        //Debug.Log("v2的模长的平方是" + v2.sqrMagnitude);
        //Debug.Log("v2的模长的平方是" + v2.sqrMagnitude);
        //Debug.Log("v2的单位化" + v2.normalized);//长度为1
        //Debug.Log(v2.x+","+v2.y);//长度为1
        //Debug.Log(v2[0] + "," + v2[1]);//长度为1
        ////公共函数
        //bool qual = v2.Equals(new Vector2(1, 1));
        //Debug.Log(qual);
        //qual = (v2 == new Vector2(2, 2));
        //Debug.Log(qual);
        //v2 = new Vector2(1,3);
        //Debug.Log("v2的单位化向量："+v2.normalized+"但是v2的向量还是："+v2);
        //v2.Normalize();
        //Debug.Log(v2);
        //v2.Set(3,4);
        //Debug.Log(v2);
        //test.position = v2; 
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 old =test.transform.position;
        float moveAmount = moveSpeed * Time.deltaTime;

        if(Input.GetKey(KeyCode.A))
        {
            test.transform.position = new Vector2(old.x-moveAmount, old.y);

        }
        if (Input.GetKey(KeyCode.D))
        {
            test.transform.position = new Vector2(old.x + moveAmount, old.y);
        }
    }
}
