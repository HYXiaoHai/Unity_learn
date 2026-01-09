using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
//
//组件 附加到游戏的所有内容的基类
//
public class No5_Commponent : MonoBehaviour
{
    public int testvallue;

    public GameObject enemyGos;
    void Start()
    {
        //组件的使用和获取
        //a.组件都是在某一个游戏物体身上挂在的，可以通过GetCommponent获取
        No5_Commponent no5_Commponent = GetComponent<No5_Commponent>();
        No2_EventFunctions no2_EventFunctions = GetComponent<No2_EventFunctions>();
        //Debug.Log(no2_EventFunctions);
        //Debug.Log(no2_EventFunctions.attectvallu);
        //Debug.Log(no5_Commponent);
        Debug.Log(no5_Commponent.testvallue);
        GameObject grisGo = GameObject.Find("Gris");
        Debug.Log(grisGo.GetComponent<SpriteRenderer>());
        Debug.Log(enemyGos.GetComponentInChildren<BoxCollider2D>());//子物体单个
        BoxCollider2D[] boxCollider2D = enemyGos.GetComponentsInChildren<BoxCollider2D>();//多个
        for (int i = 0; i < boxCollider2D.Length; i++)
        {
            Debug.Log(boxCollider2D[i]);
        }

        Debug.Log(enemyGos.GetComponentInParent<BoxCollider2D>());//父对象单个

        //b.通过其他组件查找
        SpriteRenderer sprite = grisGo.GetComponent<SpriteRenderer>();
        sprite.GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {

    }
}
