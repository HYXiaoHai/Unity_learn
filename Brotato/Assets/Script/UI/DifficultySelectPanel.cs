using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySelectPanel : MonoBehaviour
{
    public static DifficultySelectPanel Instance;
    public Transform _difficultyContent;
    public CanvasGroup _canvasGroup;
    public CanvasGroup _difficultyDetallscanvasGroup;

    public List<DifficutyData> _difficultyDatas = new List<DifficutyData>();
    public TextAsset _difficultyTextAsset;

    //
    public Transform _difficultyList;//角色列表ui
    public GameObject _difficulty_prefab;//角色预制体


    public TextMeshProUGUI _difficultyName;//难度名称
    public Image _difficultyavatar;//难度头像
    public TextMeshProUGUI _difficultyDescribe;//难度描述

    public GameObject _difficultyDetails;


    private void Awake()
    {
        Instance = this;
        _difficultyContent = GameObject.Find("DifficultyContent").transform;
        _difficultyDetallscanvasGroup = GameObject.Find("DifficultyDetalls").GetComponent<CanvasGroup>();
        _canvasGroup = GetComponent<CanvasGroup>();

        _difficultyTextAsset = Resources.Load<TextAsset>("Data/difficulty");
        _difficultyDatas = JsonConvert.DeserializeObject<List<DifficutyData>>(_difficultyTextAsset.text);

        //
        _difficultyList = GameObject.Find("DifficultyList").transform;
        _difficulty_prefab = Resources.Load<GameObject>("Prefabs/Difficulty");

        _difficultyName = GameObject.Find("DifficultyName").GetComponent<TextMeshProUGUI>();
        _difficultyavatar = GameObject.Find("Avatar_Difficulty").GetComponent<Image>();
        _difficultyDescribe = GameObject.Find("DifficultyDescribe").GetComponent<TextMeshProUGUI>();

        _difficultyDetails = GameObject.Find("WeaponDetalls");

    }
    // Start is called before the first frame update
    void Start()
    {
        foreach(DifficutyData difficutyData in _difficultyDatas)
        {
            DifficultyUI d = Instantiate(_difficulty_prefab, _difficultyList).GetComponent<DifficultyUI>();
            d.SetData(difficutyData);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
