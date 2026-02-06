using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class ProgressPanel : MonoBehaviour
{
    public static ProgressPanel instance;
    public CanvasGroup _progressPanel;

    public Button _gameProgress;//游戏进度按钮
    public Button _roleProgress;//角色进度按钮
    public Button _continueProgress;//当前游戏细节按钮
    public Button _exit;//退出按钮
    public Button _deleteSave;//初始化存档

    public CanvasGroup _gameProgressPanel;
    public CanvasGroup _roleProgressPanel;
    public CanvasGroup _continueProgressPanel;

    [Header("游戏进度")]
    public TMP_Text _allGameCount;//总的游戏次数
    public TMP_Text _allWinCount;//总的胜利次数

    [Header("角色进度")]
    public TextAsset roleTextAsset;//role json
    public List<RoleData> roleDatas;//
    public Transform roleProgressContent;//实例化细节面板的父对象
    public GameObject roleProgressDetailsPrefab;//细节面板预制体
    private bool isRoleProgress;
    //具体细节在细节面板实现。
    [Header("滚动参数")]
    public float scrollSpeed = 100f;
    public float minX = -200f; // 最小X位置
    public float maxX = 200f;  // 最大X位置

    [Header("当前进度")]//上一把游戏的信息
    public RoleData roleData;//角色信息
    public DifficutyData difficutyData;//难度信息
    public Image _continueProfile;//头像
    public TMP_Text _continueName;//名字
    public TMP_Text _continueMoneyCount;//金币数量
    public TMP_Text _continueWave;//波数
    public TMP_Text _continueDifficult;//难度
    public Transform _propUI;//展示道具父对象
    public Transform _weaponUI;//展示武器父对象
    public GameObject continueWeaponUIPrefab;//武器ui预制体
    public GameObject continuePropUIPrefab;//道具ui预制体
    public List<WeaponData> currentWeapon = new List<WeaponData>();//武器
    public List<PropData> currentProp = new List<PropData>();//道具

    private void Awake()
    {
        instance = this;
        _progressPanel = GameObject.Find("ProgressPanel").GetComponent<CanvasGroup>();

        _gameProgress = GameObject.Find("GameProgress").GetComponent<Button>();
        _roleProgress = GameObject.Find("RoleProgress").GetComponent<Button>();
        _continueProgress = GameObject.Find("ContinueProgress").GetComponent<Button>();
        _exit = GameObject.Find("Exit").GetComponent<Button>();
        _deleteSave = GameObject.Find("DeleteSave").GetComponent<Button>();

        _gameProgressPanel = GameObject.Find("GameProgressPanel").GetComponent<CanvasGroup>();
        _roleProgressPanel = GameObject.Find("RoleProgressPanel").GetComponent<CanvasGroup>();
        _continueProgressPanel = GameObject.Find("continueProgressPanel").GetComponent<CanvasGroup>();

        _allGameCount = GameObject.Find("AllGameCount").GetComponent<TMP_Text>();
        _allWinCount = GameObject.Find("AllWinCount").GetComponent<TMP_Text>();

        roleTextAsset = Resources.Load<TextAsset>("Data/role");
        roleDatas = JsonConvert.DeserializeObject<List<RoleData>>(roleTextAsset.text);
        roleProgressContent = GameObject.Find("roleProgressContent").GetComponent<Transform>();
        roleProgressDetailsPrefab = Resources.Load<GameObject>("Prefabs/roleProgressDetails");

        _continueProfile = GameObject.Find("ContinueProfile").GetComponent<Image>();
        _continueName = GameObject.Find("ContinueName").GetComponent<TMP_Text>();
        _continueMoneyCount = GameObject.Find("ContinueMoneyCount").GetComponent<TMP_Text>();
        _continueWave = GameObject.Find("ContinueWave").GetComponent<TMP_Text>();
        _continueDifficult = GameObject.Find("ContinueDifficult").GetComponent<TMP_Text>();
        _propUI = GameObject.Find("ContinueProp").GetComponent<Transform>();
        _weaponUI = GameObject.Find("ContinueWeapon").GetComponent<Transform>();
        continueWeaponUIPrefab = Resources.Load<GameObject>("Prefabs/ContinueWeaponUI");
        continuePropUIPrefab = Resources.Load<GameObject>("Prefabs/ContinuePropUI");

        //以下还没初始化
        //currentWeapon
        //currentProp
        //roleData
        //difficutyData
    }
    private void Start()
    {
        InitGameProgressPanel();
        InitRoleProgress();
        InitContinueProgress();

        // 初始化时默认关闭面板
        _progressPanel.alpha = 0;
        _progressPanel.interactable = false;
        _progressPanel.blocksRaycasts = false;

        _gameProgress.onClick.AddListener(() =>
        OnGameProgressButton()
        );
        _roleProgress.onClick.AddListener(() =>
        OnRoleProgressButton()
        );
        _continueProgress.onClick.AddListener(() =>
        OnContinueProgressButton()
        );
        _exit.onClick.AddListener(() =>
        OnExitButton()
        );
        _deleteSave.onClick.AddListener(() =>
        OnDeleteSaveButton()
        );

    }

    public void OpenProgressPanel()
    {
        _progressPanel.alpha = 1;
        _progressPanel.interactable = true;
        _progressPanel.blocksRaycasts = true;

        // 默认显示游戏进度面板
        OnGameProgressButton();

        // 刷新所有数据
        InitGameProgressPanel();
        InitRoleProgress();
        InitContinueProgress();
    }


    public void OnGameProgressButton()
    {
        _roleProgressPanel.alpha = 0;
        _roleProgressPanel.interactable = false;
        _roleProgressPanel.blocksRaycasts = false;

        _continueProgressPanel.alpha = 0;
        _continueProgressPanel.interactable = false;
        _continueProgressPanel.blocksRaycasts = false;

        _gameProgressPanel.alpha = 1;
        _gameProgressPanel.interactable = true;
        _gameProgressPanel.blocksRaycasts = true;
        isRoleProgress = false;
    }
    public void OnRoleProgressButton()
    {
        _gameProgressPanel.alpha = 0;
        _gameProgressPanel.interactable = false;
        _gameProgressPanel.blocksRaycasts = false;


        _continueProgressPanel.alpha = 0;
        _continueProgressPanel.interactable = false;
        _continueProgressPanel.blocksRaycasts = false;

        _roleProgressPanel.alpha = 1;
        _roleProgressPanel.interactable = true;
        _roleProgressPanel.blocksRaycasts = true;
        isRoleProgress = true;
    }
    public void OnContinueProgressButton()
    {
        _gameProgressPanel.alpha = 0;
        _gameProgressPanel.interactable = false;
        _gameProgressPanel.blocksRaycasts = false;

        _roleProgressPanel.alpha = 0;
        _roleProgressPanel.interactable = false;
        _roleProgressPanel.blocksRaycasts = false;

        _continueProgressPanel.alpha = 1;
        _continueProgressPanel.interactable = true;
        _continueProgressPanel.blocksRaycasts = true;
        isRoleProgress = false;
    }
    public void OnExitButton()
    {
        _progressPanel.alpha = 0;
        _progressPanel.interactable = false;
        _progressPanel.blocksRaycasts = false;

        MainMenuPanel.instance.mainMenuPanel.alpha = 1;
        MainMenuPanel.instance.mainMenuPanel.interactable = true;
        MainMenuPanel.instance.mainMenuPanel.blocksRaycasts = true;
    }
    public void OnDeleteSaveButton()
    {
        //初始化存档
        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.ResetProgress();
            Debug.Log("存档已重置");
        }
        //
        InitGameProgressPanel();
        InitRoleProgress();
        InitContinueProgress();
    }

    //初始化游戏进度
    public void InitGameProgressPanel()
    {
        if (SaveManager.Instance == null)
        {
            Debug.LogWarning("SaveManager未找到");
            return;
        }

        GameProgressData data = SaveManager.Instance.GetProgressData();
        _allGameCount.text = data.totalPlayCount.ToString();
        _allWinCount.text = data.totalWinCount.ToString();
    }
    //初始化角色进度
    public void InitRoleProgress()
    {
        //
        // 清空现有内容
        foreach (Transform child in roleProgressContent)
        {
            Destroy(child.gameObject);
        }

        if (SaveManager.Instance == null)
        {
            Debug.LogWarning("SaveManager未找到");
            return;
        }
        GameProgressData data = SaveManager.Instance.GetProgressData();
        List<int> unlockedChars = SaveManager.Instance.GetUnlockedCharacters();
        // 为每个已解锁的角色创建详情面板
        foreach (var role in roleDatas)
        {
            if (unlockedChars.Contains(role.id))
            {
                GameObject detailsObj = Instantiate(roleProgressDetailsPrefab, roleProgressContent);
                roleProgressDetails r = detailsObj.GetComponent<roleProgressDetails>();
                    // 获取该角色的统计数据
                    GameProgressData.CharacterStats stat = SaveManager.Instance.GetCharacterStats(role.id);
                    // 设置角色数据
                    r.Init(role, stat);
            }
        }
    }
    //添加的新功能：用来查看各个角色的细节面板
    //通过鼠标滚轮或者ad键来移动面板
    //机制：修改roleProgressContent的x值，使其左右移动进而带着子物体移动(roleProgressContent有Horizontal Layout Group组件 Spacing是45，细节面板的宽度是350)。
    //该机制只有在打开角色进度面板的时候执行
    void Update()
    {
        if (isRoleProgress && roleProgressContent != null)
        {
            MoveRoleDetails();
        }
    }
    // 机制：修改roleProgressContent的x值，使其左右移动进而带着子物体移动
    void MoveRoleDetails()
    {
        float moveAmount = 0;

        // 键盘A/D键控制
        if (Input.GetKey(KeyCode.A))
        {
            moveAmount = scrollSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveAmount = -scrollSpeed * Time.deltaTime;
        }

        // 鼠标滚轮控制
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) > 0.01f)
        {
            moveAmount = scroll * scrollSpeed * 5f * Time.deltaTime;
        }

        if (moveAmount != 0)
        {
            Vector3 position = roleProgressContent.localPosition;
            position.x += moveAmount;

            // 限制滚动范围
            position.x = Mathf.Clamp(position.x, minX, maxX);

            roleProgressContent.localPosition = position;
        }
    }
    //初始化当前进度
    public void InitContinueProgress()
    {
        // 清空现有内容
        foreach (Transform child in _propUI)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in _weaponUI)
        {
            Destroy(child.gameObject);
        }

        if (SaveManager.Instance == null)
        {
            Debug.LogWarning("SaveManager未找到");
            return;
        }

        GameProgressData data = SaveManager.Instance.GetProgressData();

        // 如果没有当前游戏，显示默认信息
        if (!data.hasCurrentGame || data.currentRole == null)
        {
            _continueProfile.sprite = null;
            _continueName.text = "无";
            _continueMoneyCount.text = "0";
            _continueWave.text = "0";
            _continueDifficult.text = "无";
            return;
        }

        // 设置角色信息
        roleData = data.currentRole;
        if (!string.IsNullOrEmpty(roleData.avatar))
        {
            _continueProfile.sprite = Resources.Load<Sprite>(roleData.avatar);
        }

        _continueName.text = roleData.name;

        // 设置难度信息
        if (data.currentDifficulty != null)
        {
            difficutyData = data.currentDifficulty;
            _continueDifficult.text = difficutyData.levelName;
        }
        else
        {
            _continueDifficult.text = "未知";
        }

        // 设置其他信息
        _continueMoneyCount.text = data.currentMoney.ToString();
        _continueWave.text = data.currentWave.ToString();

        // 设置道具和武器
        currentProp = data.currentProps;
        currentWeapon = data.currentWeapons;

        // 初始化道具面板
        foreach (var item in currentProp)
        {
            if (item != null && continuePropUIPrefab != null)
            {
                GameObject propUI = Instantiate(continuePropUIPrefab, _propUI);
                ContinuePropUI c = propUI.GetComponent<ContinuePropUI>();
                if (c != null)
                {
                    c.InitProp(item);
                }
            }
        }

        // 初始化武器面板
        foreach (var item in currentWeapon)
        {
            if (item != null && continueWeaponUIPrefab != null)
            {
                GameObject weaponUI = Instantiate(continueWeaponUIPrefab, _weaponUI);
                ContinueWeaponUI c = weaponUI.GetComponent<ContinueWeaponUI>();
                if (c != null)
                {
                    c.InitWeapon(item);
                }
            }
        }
    }
}
