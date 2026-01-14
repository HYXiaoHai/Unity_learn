using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
//变换组件
//
public class No6_Transform : MonoBehaviour
{
    public GameObject grisGo;
    public Transform m_Transform;
    public float movespeed;
    // Start is called before the first frame update
    void Start()
    {
       
        Debug.Log(transform); //对象本身的transform
        Debug.Log(grisGo.transform);//引用外部的

        Transform grisTRans = grisGo.transform;
        Debug.Log("Gris下的子对象个数是："+grisTRans.childCount);
        Debug.Log("Grips世界空间中的位置是："+grisTRans.position);

        Debug.Log("Grips以四元数表示的旋转是："+grisTRans.rotation);
        Debug.Log("Grips以欧拉角表示的旋转是："+grisTRans.eulerAngles);

        Debug.Log("Grips父对象的transform："+grisTRans.parent);
        Debug.Log("Grips相对于父对象的坐标是："+grisTRans.localPosition);
        Debug.Log("Grips相对于父对象的四元数表示的旋转是：" + grisTRans.localRotation);
        Debug.Log("Grips相对于父对象的欧拉角表示的旋转是：" + grisTRans.localEulerAngles);
        Debug.Log("Grips相对于父对象的变换缩放是：" + grisTRans.localScale);

        Debug.Log("Grips的自身坐标正方向是（蓝色z轴）：" + grisTRans.forward);//local的正方向
        //Vector3.forward; 世界坐标的正方向。
        Debug.Log("Grips的自身坐标正右方（x）：" + grisTRans.right);//local的正右方（x）
        Debug.Log("Grips的自身坐标正上方是（y）：" + grisTRans.up);//local的正上方（y）
        //共有方法
        //3.查找
        Debug.Log("当前脚本挂载对象的叫Gris的子物体"+transform.Find("Gris"));
        Debug.Log("当前脚本挂载对象下的第一个子对象的transform"+transform.GetChild(0));
        Debug.Log("子对象的数组下标位置（索引）"+grisTRans.GetSiblingIndex());
        //静态方法
        Destroy(grisGo);
        GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        //0.移动（按照世界坐标系移动,不填）实际情况按照自身坐标移动
        grisGo.transform.Translate(Vector2.left);

        grisGo.transform.Translate(-grisGo.transform.right);
        //1.移动（按照世界坐标系移动，指定世界坐标系）实际情况按照世界坐标移动
        grisGo.transform.Translate(Vector2.left*movespeed,Space.World);
        //2.移动（按照世界坐标系移动，指定自身坐标系）实际情况自身坐标移动
        grisGo.transform.Translate(Vector2.left*movespeed,Space.Self);
        //3.移动（按照自身坐标系移动，指定世界坐标系）实际情况按照自身坐标移动
        grisGo.transform.Translate(-grisGo.transform.right*movespeed,Space.World);
        //4.移动（按照自身坐标系移动，指定自身坐标系）实际情况世界坐标移动（一般不使用）
        grisGo.transform.Translate(-grisGo.transform.right * movespeed,Space.Self);
  
        //旋转
        grisGo.transform.Rotate(new Vector3(0,0,1));//2d场景一般旋转z轴 
        grisGo.transform.Rotate(Vector3.forward,1*Time.deltaTime); 
    }
}
