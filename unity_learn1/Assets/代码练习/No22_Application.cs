using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class No22_Application : MonoBehaviour
{
    private void Start()
    {
        //游戏数据文件夹路径（只读，加密压缩）
        Debug.Log(Application.dataPath+"/新建文档.txt");
        //持久化文件夹路径
        Debug.Log(Application.persistentDataPath);
        //StreamingAssets
        Debug.Log(Application.streamingAssetsPath);
        //临时文件夹
        Debug.Log(Application.temporaryCachePath);
        //控制是否是后台运行
        Debug.Log(Application.runInBackground);
        //打开网址
        Application.OpenURL("https://sapce.bilibili.com/67744423");
        //退出游戏
        Application.Quit();
    }
}
