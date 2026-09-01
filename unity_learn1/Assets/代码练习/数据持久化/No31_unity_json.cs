using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class No31_unity_json : MonoBehaviour
{
    public Booker book = new Booker();
    public Books bookData = new Books();
    public  string filename;
    private void Start()
    {
        Debug.Log(JsonUtility.ToJson(book));
        Debug.Log(JsonUtility.ToJson(bookData));

        string excelFilePath = Application.streamingAssetsPath + "/" + filename + ".json";//处理路径
        //文件夹是否存在
        if (!Directory.Exists(Application.streamingAssetsPath))
        {
            //创建文件夹
            Directory.CreateDirectory(Application.streamingAssetsPath);
        }
        string jsonInfo = JsonUtility.ToJson(bookData);//转化格式
        File.WriteAllText(excelFilePath, jsonInfo);//存储

    }
}
[Serializable]
public class Books
{
    public List<Booker> book_ = new List<Booker>();//多数据结构 需要定义新的类
}