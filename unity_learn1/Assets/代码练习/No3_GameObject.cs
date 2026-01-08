using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;
//
//游戏物体
//
public class No3_GameObject : MonoBehaviour
{
    public GameObject grisGo;
    public
    // Start is called before the first frame update
    void Start()
    {
        //1.创建方式
        //a.使用构造函数（声明+实例化）创建一个空的游戏对象。
        GameObject myGo = new GameObject("MyGameObject");
        //b.使用现有的预制体（游戏物体）资源或者游戏场景已有的物体来实例化
        Instantiate(grisGo);
        //c.使用特别的api创建基本的游戏物体类型
        GameObject.CreatePrimitive(PrimitiveType.Capsule);

        //2.物体的查找
        //this;//当前组件
        this.test();
        //this.gameObject
        gameObject.SetActive(true);//物体激活
        Debug.Log("当前游戏物体标签：" + gameObject.tag);
        Debug.Log("当前游戏物体层级：" + gameObject.layer);
        //有引用
        //对自己 this.gameobject
        //对其他游戏物体
        Debug.Log("gris的物体状态是" + grisGo.activeSelf);
        //没有引用
        //对其它物体
        //a.通过物体名称
        GameObject maincamera = GameObject.Find("Main Camera");
        Debug.Log("maincamera的标签是" + maincamera.tag);
        maincamera.name = "Camera";
        //b.通过标签进行查找(单个查找)
        GameObject camare = GameObject.FindGameObjectWithTag("MainCamera");
        Debug.Log("物体的名称是：" + camare.name);
        //c.通过类型查找
        No2_EventFunctions no2_EventFunctions = GameObject.FindObjectOfType<No2_EventFunctions>();
        Debug.Log("物体的名称是：" + no2_EventFunctions.name);
        //d.多数查找与获取
        //通过标签进行查找(多数查找)
        GameObject[] enemyGos = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemyGos.Length; i++)
        {
            Debug.Log("查找敌人的名称是：" + enemyGos[i].name);
        }
        Debug.Log("---------------------------------------------");
        BoxCollider2D[] colliders = GameObject.FindObjectsOfType<BoxCollider2D>();
        for (int i = 0; i < colliders.Length; i++)
        {
            Debug.Log(colliders[i].name);

        }
    }

        // Update is called once per frame
        void Update()
        {

        }
        void test()
        {
            Debug.Log("test()");
        }
    }
