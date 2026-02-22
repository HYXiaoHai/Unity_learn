using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerChange:ScriptableWizard //对话框
{
    [MenuItem("Tools/CreatWizard", false, 12)]//快捷键Ctr+t
    static void CreatWizard()
    {
        ScriptableWizard.DisplayWizard<PlayerChange>("统一修改玩家健康","MybuttonName","OtherButton");
    }

    public int changeHealthValue = 10;
    public int changeFlashSpeedValue = 10;

    //每次被创建出来的时候
    private void OnEnable()
    {
        changeHealthValue = EditorPrefs.GetInt("changeHealthValue");
        changeFlashSpeedValue = EditorPrefs.GetInt("changeFlashSpeedValue");
    }

    void OnWizardCreate()//应用并关闭
    {
        GameObject[] players = Selection.gameObjects;
        EditorUtility.DisplayProgressBar("进度", "0/" + players.Length + "完成修改值", 0);//增加进度条
        int count = 0;
        foreach (GameObject item in players)
        {
            Player player = item.GetComponent<Player>();
            Undo.RecordObject(player, "AddValue");//记录更改
            player.startingHealth += changeHealthValue;
            player.flashSpeed += changeFlashSpeedValue;
            count++;
        EditorUtility.DisplayProgressBar("进度", count+"/" + players.Length + "完成修改值", (float)count/players.Length);
        }
        EditorUtility.ClearProgressBar();//完成后关闭进度条
        ShowNotification(new GUIContent(Selection.gameObjects.Length + "个物体被修改了"));
    }
    private void OnWizardOtherButton()//不关闭
    {
        Debug.Log("选择了otherbutton");
        OnWizardCreate();
    }

    void OnWizardUpdate()//应用并关闭
    {
        Debug.Log("每一次改变值就会调用");
        if(Selection.objects.Length>0)
        {
            helpString = "您选择了对象";
        }
        else
        {
            errorString = "至少选择一个敌人";
        }

        EditorPrefs.SetInt("changeHealthValue", changeHealthValue);//保存更改的数值
        EditorPrefs.SetInt("changeFlashSpeedValue", changeFlashSpeedValue);

    }
    private void OnSelectionChange()
    {
        OnWizardUpdate();
    }
}
