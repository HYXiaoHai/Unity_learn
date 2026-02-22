using UnityEditor; //引用
using UnityEngine;
public class Tools
{
    //增加工具栏信息
    [MenuItem("Tools/test/test1")]
    static void Test()
    {
        Debug.Log("Tools/test");
    }
    [MenuItem("Tools/test/test2")]
    static void Test2()
    {
        Debug.Log("Tools/test");
    }
    [MenuItem("Window/test")]
    static void WindowTest()
    {
        Debug.Log("WindowTest");
    }

    //每一个菜单栏的priority优先级默认1000
    //优先级相差11，就相当于分类
    [MenuItem("Tools/test1",false,1)]
    static void test1()
    {
        Debug.Log("Tools/test");
    }
    [MenuItem("Tools/test2",false,12)]
    static void test2()
    {
        Debug.Log("Tools/test");
    }
    [MenuItem("Tools/test3",false,23)]
    static void test3()
    {
        Debug.Log("Tools/test");
    }
    [MenuItem("GameObject/test",false,9)]
    static void test4()
    {
        Debug.Log("Tools/test");
    }

    [MenuItem("Tools/show info",false,10)]
    static void ShowInfo()
    {
        Debug.Log(Selection.activeGameObject.name);//获取第一个选择的物品
        Debug.Log(Selection.objects.Length);
    }



    [MenuItem("Tools/MyDelet _%t", true, 11)]//快捷键Ctr+t
    static bool MyDeleteValidate()
    {
        if (Selection.objects.Length > 0)
            return true;
        else
            return false;
    }

    //[MenuItem("Tools/MyDelet",false,11)]
    //[MenuItem("Tools/MyDelet _t",false,11)]//快捷键t
    //[MenuItem("Tools/MyDelet _%t",false,11)]//快捷键Ctr+t
    //[MenuItem("Tools/MyDelet _#t",false,11)]//快捷键shift+t
    //[MenuItem("Tools/MyDelet _&t",false,11)]//快捷键alt+t

    //以上的false是是否开启验证
    [MenuItem("Tools/MyDelet _%t",false, 11)]//快捷键Ctr+t
    static void delete()
    {
        foreach (Object o in Selection.objects)
        {
            //GameObject.DestroyImmediate(o);//不可以撤销的
            Undo.DestroyObjectImmediate(o);//可以撤销的
        }
        //需要把删除操作注册到 操作记录里面

    }

}
