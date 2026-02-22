using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool isPaused = true;
    public CanvasGroup menuGo;

    public int targetSize = 1;
    public GameObject[] targetsGO;
    private void Awake()
    {
        Instance = this;
        Pause();
    }
    public void Pause()
    {
        isPaused = true;
        menuGo.alpha = 1;
        menuGo.interactable = true;
        menuGo.blocksRaycasts = true;
        Time.timeScale = 0;
        Cursor.visible = true;//显示鼠标
    }
    public void UnPause()
    {
        isPaused = false;
        menuGo.alpha = 0;
        menuGo.interactable = false;
        menuGo.blocksRaycasts = false;
        Time.timeScale = 1;
        Cursor.visible = false;//不显示鼠标
    }

    public Save CreatSaveGO()
    {
        Save save = new Save();
        foreach (var targetGo in targetsGO)
        {
            TargetManager targetmanage = targetGo.GetComponent<TargetManager>();
            if(targetmanage.currenttarget != null)
            {
                save.livingTargetPositions.Add(targetmanage.targetPosition);
                int type = targetmanage.currenttarget.GetComponent<MonsterManager>().monsterType;
                save.livingMonsterTypes.Add(type);
            }
        }
        save.shootNum = UIManager.Instance.shootNume;
        save.score = UIManager.Instance.score;

        return save;
    }
    //通过读取信息初始化状态
    private void SetGame(Save save)
    {
        foreach (GameObject targetGO in targetsGO)
        {
            targetGO.GetComponent<TargetManager>().UpadateMonster();
        }
        for(int i =0;i<save.livingTargetPositions.Count;i++)
        {
            int pos = save.livingTargetPositions[i];
            int type = save.livingMonsterTypes[i];

            targetsGO[pos].GetComponent<TargetManager>().ActivateMonsterByType(type);
        }
        UIManager.Instance.score = save.score;
        UIManager.Instance.shootNume = save.shootNum;
        ContinueGame();
    }

    //二进制方法
    private void SaveByBin()
    {
        //序列化过程
        Save save = CreatSaveGO();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fileStream = File.Create(Application.dataPath + "/StreamingFile"+"/byBin.txt");
        //用二进制格式化程序 (创建的文件流，需要序列化的对象)
        bf.Serialize(fileStream,save);
        fileStream.Close();

        if (File.Exists(Application.dataPath + "/StreamingFile" + "/byBin.txt"))
        {
            UIManager.Instance.ShowMessage("保存成功");
        }
    }
    private void LoadByBin()
    {
        if (File.Exists(Application.dataPath + "/StreamingFile" + "/byBin.txt"))
        {
            //反序列化
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fileStream = File.Open(Application.dataPath + "/StreamingFile" + "/byBin.txt", FileMode.Open);
            Save save = (Save)bf.Deserialize(fileStream);
            fileStream.Close();
            UIManager.Instance.ShowMessage("加载成功");
            SetGame(save);
        }
        else
        {
            UIManager.Instance.ShowMessage("加载失败");
            ContinueGame();
        }
    }
    //XML
    private void SaveByXml()
    {
        //序列化过程
        Save save = CreatSaveGO();
        string filepath = Application.dataPath + "/StreamingFile" + "/byXml.xml";
        XmlDocument xmlDoc = new XmlDocument();
        //创建根节点
        XmlElement root = xmlDoc.CreateElement("Save");
        //设置根节点的值
        root.SetAttribute("name", "savefile1");
        XmlElement target;
        XmlElement targetPosition;
        XmlElement monsterType;
        //root - target -(targetposition monsterType)
        //细节设置
        //CreateElement 创建节点（名字）
        //InnerText 设置值（对应内容）
        for (int i = 0; i < save.livingTargetPositions.Count; i++)
        {
            target = xmlDoc.CreateElement("target");
            targetPosition = xmlDoc.CreateElement("position");
            targetPosition.InnerText = save.livingTargetPositions[i].ToString();

            monsterType = xmlDoc.CreateElement("monsterType");
            monsterType.InnerText = save.livingMonsterTypes[i].ToString();

            target.AppendChild(targetPosition);
            target.AppendChild(monsterType);
            root.AppendChild(target);
        }
        //分数节点
        XmlElement shootNum = xmlDoc.CreateElement("shootNum");
        shootNum.InnerText = save.shootNum.ToString();

        XmlElement score = xmlDoc.CreateElement("score"); 
        score.InnerText  = save.score.ToString();

        root.AppendChild(shootNum);
        root.AppendChild(score);

        xmlDoc.AppendChild(root);//设置根结点
        //xmlDoc -- root -- target -- (targetposition  monsterType)
        //               |
        //               |--shootNum
        //               |--score
        xmlDoc.Save(filepath);

        if (File.Exists(Application.dataPath + "/StreamingFile" + "/byXml.xml"))
        {
            UIManager.Instance.ShowMessage("保存成功");
        }
    }
    private void LoadByXml()
    {
        Save save = new Save();
        string filePath = Application.dataPath + "/StreamingFile" + "/byXml.xml";
        if (File.Exists(Application.dataPath + "/StreamingFile" + "/byXml.xml"))
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);

            //通过节点来获取元素，结果为xmlNodeList类型
            XmlNodeList targets = xmlDoc.GetElementsByTagName("target");
            //遍历
            if (targets.Count != 0)
            {
                foreach (XmlNode target in targets)
                {
                    XmlNode targetPosition = target.ChildNodes[0];
                    int targetPositoinIndex = int.Parse(targetPosition.InnerText);
                    save.livingTargetPositions.Add(targetPositoinIndex);//读取一个数据 保存到save中

                    XmlNode monstrType = target.ChildNodes[1];
                    int monstrtype = int.Parse(monstrType.InnerText);
                    save.livingMonsterTypes.Add(monstrtype);//读取一个数据 保存到save中
                }
            }

            XmlNodeList shootnum = xmlDoc.GetElementsByTagName("shootNum");
            int shootNum = int.Parse(shootnum[0].InnerText);
            save.shootNum = shootNum;

            XmlNodeList Score = xmlDoc.GetElementsByTagName("score");
            int score = int.Parse(Score[0].InnerText);
            save.score = score;

            SetGame(save);
        }
        else
        {
            UIManager.Instance.ShowMessage("加载失败");
            ContinueGame();
        }
    }
    //json
    private void SaveByJson()
    {
        //序列化过程
        Save save = CreatSaveGO();
        string filepath = Application.dataPath + "/StreamingFile" + "/byJson.json";
        string saveJsonstr = JsonMapper.ToJson(save);
        StreamWriter sw = new StreamWriter(filepath);
        sw.Write(saveJsonstr);
        sw.Close();
        if (File.Exists(Application.dataPath + "/StreamingFile" + "/byJson.json"))
        {
            UIManager.Instance.ShowMessage("保存成功");
        }
    }
    private void LoadByJson()
    {
        string filepath = Application.dataPath + "/StreamingFile" + "/byJson.json";
        if (File.Exists(filepath))
        {
            StreamReader sr = new StreamReader(filepath);
            string jsonstr = sr.ReadToEnd();
            sr.Close();
            Save save = JsonMapper.ToObject<Save>(jsonstr);
            SetGame(save);
        }
        else
        {
            UIManager.Instance.ShowMessage("加载失败");
            ContinueGame();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused == true)
            {
                //正在暂停
                ContinueGame();
            }
            else
            {
                Pause();
            }
        }
    }

    public void ContinueGame()
    {
        UnPause();
        UIManager.Instance.ShowMessage("");

    }
    public void NewGame()
    {
        foreach (var targetGO in targetsGO)
        {
            targetGO.GetComponent<TargetManager>().UpadateMonster();
        }
        UIManager.Instance.shootNume = 0;
        UIManager.Instance.score = 0;
        UIManager.Instance.ShowMessage("");
        UnPause();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void SaveGame()
    {
        SaveByXml();
    }
    public void LoadGame()
    {
        LoadByXml();
    }
}
