using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
public class No28_IO : MonoBehaviour
{
    public string filename;
    private void Start()
    {
        string FilePath = Application.streamingAssetsPath + "/" + filename + "0.txt";
        File.WriteAllText(FilePath, "今天我最帅");

        FilePath = Application.streamingAssetsPath + "/" + filename + "1.txt";
        FileStream file = new FileStream(FilePath,FileMode.OpenOrCreate);
        byte[] bytes =Encoding.UTF8.GetBytes("今天变帅了");
        file.Write(bytes,0,bytes.Length);
        file.Close();
    }
}
