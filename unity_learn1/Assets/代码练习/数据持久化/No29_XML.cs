using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
//[Serializable]
//public class Booker
//{
//    public int id;
//    public string name;
//    public string editor;
//}
public class No29_XML : MonoBehaviour
{
    public List<Booker> books = new List<Booker>();
    public string Filename;
    void Start()
    {
        XmlDocument xml = new XmlDocument();
        //设置结点
        XmlElement root = xml.CreateElement("Root");//根节点
        root.SetAttribute("Root", "技能");
        foreach (var item in books)
        {
            XmlElement book = xml.CreateElement("Book");
            book.SetAttribute("ID", item.id.ToString());
            root.AppendChild(book);


            XmlElement name =xml.CreateElement("Name");
            book.AppendChild(name);
            name.InnerText = item.name;

            XmlElement author =xml.CreateElement("Author");
            book.AppendChild(author);
            author.InnerText = item.editor;
        }
        xml.AppendChild(root);
        string filePath = Application.streamingAssetsPath + "/" + Filename + ".xml";
        if(Directory.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
        }

        xml.Save(filePath);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
