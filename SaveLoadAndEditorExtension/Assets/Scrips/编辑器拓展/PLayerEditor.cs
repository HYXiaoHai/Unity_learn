using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class PLayerEditor
{
    [MenuItem("CONTEXT/PlayerHealth/InitHealthAndSpeed")]//在PlayerHealth脚本上增加右键的方法
    static void InitHelthAndSpeed(MenuCommand menu)//menucommand是当前正在操作的组件
    {
        Player health = menu.context as Player;//拿到组件
        health.startingHealth = 200;

        health.flashSpeed = 10;
        Debug.Log(menu.context.name);
        Debug.Log("initHelthAndSpeed");
    }

    [MenuItem("CONTEXT/Rigidbody/Clear")]
    static void ClearMassAndGravity(MenuCommand menu)//关闭刚体上的重力和质量
    {
        Rigidbody body = menu.context as Rigidbody;
        body.mass = 0;
        body.useGravity = false;
    }
}
