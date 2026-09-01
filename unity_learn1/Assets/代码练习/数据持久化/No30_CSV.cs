using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class No30_CSV : MonoBehaviour
{
    public List<Booker> books = new List<Booker>();
    public string Filename;
    void Start()
    {
        string excelFilePath = Application.streamingAssetsPath + "/" + Filename + ".csv";//处理路径
        //文件夹是否存在
        if (!Directory.Exists(Application.streamingAssetsPath))
        {
            //创建文件夹
            Directory.CreateDirectory(Application.streamingAssetsPath);
        }
        StreamWriter sw = new StreamWriter(excelFilePath);
        sw.WriteLine("ID,Name,Author");
        for(int i =0;i<books.Count;i++)
        {
            sw.WriteLine($"{books[i].id},{books[i].name},{books[i].editor}");
        }
        //推送到流文件中
        sw.Flush();
        sw.Close();
    }
}
