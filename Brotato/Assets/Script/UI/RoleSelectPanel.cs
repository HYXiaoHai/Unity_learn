using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using TMPro;
using UnityEngine.UI;
public class RoleSelectPanel : MonoBehaviour
{
    public static RoleSelectPanel Instance;
    public CanvasGroup _canvasGroup;
    public CanvasGroup _contentCanvasGroup;
    
    public List<RoleData> roleDatas = new List<RoleData>();//角色数据信息
    private TextAsset roleTextAsset;//文件读取json文件

    //组件
    public Transform  _roleList;//角色列表ui
    public GameObject _role_prefab;//角色预制体

    public TextMeshProUGUI _roleName;//角色名称
    public Image _avatar;//角色头像
    public TextMeshProUGUI _roleDescribe;//角色描述
    public TextMeshProUGUI _text3;//通关记录

    public GameObject _roleDetails;

    private void Awake()
    {
        Instance = this;

        _roleList = GameObject.Find("RoleList").transform;
        _role_prefab = Resources.Load<GameObject>("Prefabs/Role");
        _contentCanvasGroup = GameObject.Find("RoleContent").GetComponent<CanvasGroup>();

        //读取json文件，并转化为对象。
        roleTextAsset = Resources.Load<TextAsset>("Data/role");
        roleDatas = JsonConvert.DeserializeObject<List<RoleData>>(roleTextAsset.text);

        _roleName = GameObject.Find("RoleName").GetComponent<TextMeshProUGUI>();
        _avatar = GameObject.Find("Avatar_Role").GetComponent<Image>();
        _roleDescribe = GameObject.Find("RoleDescribe").GetComponent<TextMeshProUGUI>();
        _text3 = GameObject.Find("Text3").GetComponent<TextMeshProUGUI>();

        _canvasGroup = GetComponent<CanvasGroup>();
        _roleDetails = GameObject.Find("RoleDetalls");
    }
    void Start()
    {
        foreach (RoleData roledata in roleDatas)
        {
            RoleUI r = Instantiate(_role_prefab,_roleList).GetComponent<RoleUI>();
            r.SetData(roledata);
        }
    }
}
