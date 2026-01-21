using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectPanel : MonoBehaviour
{
    public static WeaponSelectPanel Instance;
    public Transform _weaponContent;
    public CanvasGroup _canvasGroup;
    public CanvasGroup _WeaponDetallscanvasGroup;

    
    public List<WeaponData> weaponDatas = new List<WeaponData>();//角色数据信息
    private TextAsset weaponTextAsset;//文件读取json文件

    //组件
    public Transform _weaponList;//角色列表ui
    public GameObject _weapon_prefab;//角色预制体

    public TextMeshProUGUI _weaponName;//武器名称
    public Image _weaponavatar;//武器头像
    public TextMeshProUGUI _weaponType;//武器类型
    public TextMeshProUGUI _weaponDescribe;//武器描述

    public GameObject _weaponDetails;

    private void Awake()
    {
        Instance = this;

        _canvasGroup = GetComponent<CanvasGroup>();
        _weaponContent = GameObject.Find("WeaponContent").transform;
        _WeaponDetallscanvasGroup = GameObject.Find("WeaponDetalls").GetComponent<CanvasGroup>();

        //读取json文件，并转化为对象。
        weaponTextAsset = Resources.Load<TextAsset>("Data/weapon");
        weaponDatas = JsonConvert.DeserializeObject<List<WeaponData>>(weaponTextAsset.text);//反序列化

        //
        _weaponList = GameObject.Find("WeaponList").transform;
        _weapon_prefab = Resources.Load<GameObject>("Prefabs/Weapon");

        _weaponName = GameObject.Find("WeaponName").GetComponent<TextMeshProUGUI>();
        _weaponavatar = GameObject.Find("Avatar_Weapon").GetComponent<Image>();
        _weaponType = GameObject.Find("WeaponType").GetComponent<TextMeshProUGUI>();
        _weaponDescribe = GameObject.Find("WeaponDescribe").GetComponent <TextMeshProUGUI>();

        _weaponDetails = GameObject.Find("WeaponDetalls");

            
    }
    private void Start()
    {
        foreach (WeaponData weapondata in weaponDatas)
        {
           WeaponUI wea =  Instantiate(_weapon_prefab, _weaponList).GetComponent<WeaponUI>();
            wea.SetData(weapondata);
        }
    }
}
