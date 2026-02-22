using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class MyWindow : EditorWindow
{
    [MenuItem("Window/show myWindow")]
    static void ShowMyWindow()
    {
        MyWindow window = GetWindow<MyWindow>();//得到窗口
        window.Show();//展示
    }
    private string name ="";
    private void OnGUI()
    {
        GUILayout.Label("这是我的窗口");
       name = GUILayout.TextField(name);//输入的文本
        if(GUILayout.Button("创建物体"))
        {
            GameObject gameObject = new GameObject(name);
            Undo.RegisterCreatedObjectUndo(gameObject,"create gameobject");//注册创建对象的记录
        }
    }
}
