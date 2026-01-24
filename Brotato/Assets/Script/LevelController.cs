using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance;
    public float waveTimer;

    public GameObject _failPanel;
    public GameObject _successPanel;

    public List<EnemyBase> enemy_list;//敌人预制体
    public Transform _map;//地图
    public float safeDistance = 1f;//生成敌人距离边界的安全距离
    
    public GameObject enemy1_prefab;//敌人1
    public GameObject enemy2_prefab;//敌人2
    public GameObject enemy3_prefab;//敌人3
    public GameObject enemy4_prefab;//敌人4
    public GameObject enemy5_prefab;//敌人5
    public GameObject redfork_prefab;//红叉预制体
    
    public TextAsset LevelTextAsset;//json
    public List<LevelData>levelDatas = new List<LevelData>();//关卡信息

    public LevelData currentLevelData;//当前的关卡
    public EnemyData currentenemyData;//当前生成的敌人
    private string level;
    public Dictionary<string,GameObject> enemyPrefabDic = new Dictionary<string, GameObject>();

    private void Awake()
    {
        Instance = this;
        _failPanel = GameObject.Find("FailPanel");
        _successPanel = GameObject.Find("SuccesPanel");

        enemy1_prefab = Resources.Load<GameObject>("Prefabs/Enemy1");
        enemy2_prefab = Resources.Load<GameObject>("Prefabs/Enemy2");
        enemy3_prefab = Resources.Load<GameObject>("Prefabs/Enemy3");
        enemy4_prefab = Resources.Load<GameObject>("Prefabs/Enemy4");
        enemy5_prefab = Resources.Load<GameObject>("Prefabs/Enemy5");



        redfork_prefab = Resources.Load<GameObject>("Prefabs/RedFork");
        _map = GameObject.Find("Map").GetComponent<Transform>();
        //Debug.Log(GameManage.Instance.currentWave);
        //level = "Data/level0"+(GameManage.Instance.currentWave - 1).ToString();
        level = "Data/level1";
        LevelTextAsset = Resources.Load<TextAsset>(level);
        levelDatas = JsonConvert.DeserializeObject<List<LevelData>>(LevelTextAsset.text);

        enemyPrefabDic.Add("enemy1",enemy1_prefab);
        enemyPrefabDic.Add("enemy2",enemy2_prefab);
        enemyPrefabDic.Add("enemy3",enemy3_prefab);
        enemyPrefabDic.Add("enemy4",enemy4_prefab);
        enemyPrefabDic.Add("enemy5",enemy5_prefab);
    }
    void Update()
    {
        if (waveTimer > 0)
        {
            waveTimer -= Time.deltaTime;
            if (waveTimer <= 0)
            {
                waveTimer = 0;
                GoodGame();
            }
        }
        GamePanel.instance.RenewCountDown(waveTimer);
    }
    void Start()
    {
        currentLevelData = levelDatas[1];//保存当前关卡信息
        waveTimer = currentLevelData.waveTimer;//当前关卡的信息。

        GenerateEnemy();

    }
    //生成敌人
    public void GenerateEnemy()
    {
        //遍历所有敌人
        foreach (WaveData waveData in currentLevelData.enemys)
        {
            //变量敌人数量
            for (int i = 0; i < waveData.count; i++)
            {
                StartCoroutine(SwawnEnemies(waveData));
            }
        }
    }

    EnemyData MakecurrenEnemy(string name)
    {
        foreach (EnemyData enemy in GameManage.Instance.enemyDatas)
        {
            if(enemy.name == name)
            {
                return enemy;
            }
        }
        return null;
    }

    IEnumerator SwawnEnemies(WaveData waveData)
    {

        yield return new WaitForSeconds(waveData.timeAxis);
        
        if(waveTimer > 0 && !Player.instance.isDead)
        {
            var spawnPoint = GetRandomPosition(_map.GetComponent<SpriteRenderer>().bounds);
            GameObject go =  Instantiate(redfork_prefab, spawnPoint, Quaternion.identity);
            yield return new WaitForSeconds(1);
            Destroy(go);
            if(waveTimer > 0 && !Player.instance.isDead)
            {
                EnemyBase enemy = Instantiate(enemyPrefabDic[waveData.enemyName], spawnPoint, Quaternion.identity).GetComponent<EnemyBase>();
                currentenemyData = MakecurrenEnemy(waveData.enemyName);
                enemy.InitEnemy(currentenemyData);
                enemy_list.Add(enemy);
            }
        }
    }

    private Vector3 GetRandomPosition(Bounds bounds)
    {
        float randomx = Random.Range(bounds.min.x + safeDistance, bounds.max.x - safeDistance);
        float randomy = Random.Range(bounds.min.y + safeDistance, bounds.max.y - safeDistance);

        float randomz = 0f;

        return new Vector3(randomx, randomy, randomz);
    }



    //游戏胜利
    public void GoodGame()
    {
        _successPanel.GetComponent<CanvasGroup>().alpha = 1;

        //StartCoroutine(GoMenu());
        GoShop();
        //todo 所有敌人消失
        for (int i =0; i<enemy_list.Count;i++)
        {
            if(enemy_list[i] != null)
            enemy_list[i].Dead();
        }
    }
    public void WinGame()
    {
        _successPanel.GetComponent<CanvasGroup>().alpha = 1;

        StartCoroutine(GoMenu());

        //todo 所有敌人消失
        for (int i =0; i<enemy_list.Count;i++)
        {
            if(enemy_list[i] != null)
            enemy_list[i].Dead();
        }
    }

    //波次完成

    //游戏失败
    public void BadGame()
    {
        _failPanel.GetComponent<CanvasGroup>().alpha = 1;

        StartCoroutine(GoMenu());
        //todo 所有敌人消失
        for (int i = 0; i < enemy_list.Count; i++)
        {
            if (enemy_list[i] != null)
                enemy_list[i].Dead();
        }

    }

    void GoShop()
    {
        SceneManager.LoadScene(4);
    }

    IEnumerator GoMenu()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);
    }
}
