using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleSelectPanel : MonoBehaviour
{
    public static RoleSelectPanel instance;

    public List<RoleData> roleDatas = new List<RoleData>();//角色数据信息
    public TextAsset roleTextAsset;//文件读取

    private void Awake()
    {
        instance = this;

        //读取json文件，并转化为对象。
        roleTextAsset = Resources.Load<TextAsset>("Data/role");
        //roleTextAsset = JsonConvert.DeserializeObject<List<RoleData>>(roleTextAsset.text);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
