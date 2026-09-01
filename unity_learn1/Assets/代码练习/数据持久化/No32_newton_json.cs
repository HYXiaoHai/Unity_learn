using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class No32_newton_json : MonoBehaviour
{
    public List<Booker> books = new List<Booker>();
    public string excelFilename;
    // Start is called before the first frame update
    void Start()
    {
       string booksInfo = JsonConvert.SerializeObject(books,Formatting.Indented);
        string excelFilePath = Application.streamingAssetsPath + "/" + excelFilename + "newton.json";//处理路径
        //文件夹是否存在
        if (!Directory.Exists(Application.streamingAssetsPath))
        {
            //创建文件夹
            Directory.CreateDirectory(Application.streamingAssetsPath);
        }
        File.WriteAllText(excelFilePath, booksInfo);//存储
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
