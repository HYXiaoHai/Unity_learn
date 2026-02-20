using OfficeOpenXml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
[Serializable]
public class Booker
{
    public int id;
    public string name;
    public string editor;
}

public class No26_Excel : MonoBehaviour
{
    public List<Booker> books = new List<Booker>();
    public string excelFilename, sheetName;
    private void Start()
    {
        string excelFilePath = Application.streamingAssetsPath + "/" + excelFilename + ".xlsx";//处理路径
        //文件夹是否存在
        if(!Directory.Exists(Application.streamingAssetsPath))
        {
            //创建文件夹
            Directory.CreateDirectory(Application.streamingAssetsPath);
        }
        //获取文件信息
        FileInfo fileInfo = new FileInfo(excelFilename);
        if(!fileInfo.Exists)
        {
            fileInfo = new FileInfo(excelFilePath);
        }
        //编辑文件信息
        using (ExcelPackage package = new ExcelPackage(fileInfo))
        {
            //编辑工作部
            ExcelWorksheet excelWorksheet = package.Workbook.Worksheets.Add(sheetName);//添加表
            //处理标题
            excelWorksheet.Cells["A1"].Value = "ID";
            excelWorksheet.Cells["B1"].Value = "Name";
            excelWorksheet.Cells["C1"].Value = "Author";
            //处理数据内容
            for(int i =0;i<books.Count;i++)
            {
                excelWorksheet.Cells[i + 2, 1].Value = books[i].id;
                excelWorksheet.Cells[i + 2, 2].Value = books[i].name;
                excelWorksheet.Cells[i + 2, 3].Value = books[i].editor;
            }
            //保存存储
            package.Save();
        }
    }
}
