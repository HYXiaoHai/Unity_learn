using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GameManage : MonoBehaviour
{
    public static GameManage Instance;
    public RoleData currentRole;//当前角色
    public List<WeaponData> currentWeapon = new List<WeaponData>();//记录当前所有武器
    public DifficutyData currentDifficulty;//
    public int currentWave = 1;
    
    public List<EnemyData> enemyDatas = new List<EnemyData>();//
    public TextAsset enemyTextAsset;

    private void Awake()
    {
        // 完整的单例模式实现
        if (Instance == null)
        {
            Instance = this;
            UnityEngine.Debug.Log("gamemanage instance :"+Instance);
            DontDestroyOnLoad(gameObject);

        }
        else if (Instance != this)
        {
            // 如果已存在实例，销毁新创建的
            Destroy(gameObject);
            return;
        }

        enemyTextAsset = Resources.Load<TextAsset>("Data/enemy");
        if (enemyTextAsset != null)
        {
            enemyDatas = JsonConvert.DeserializeObject<List<EnemyData>>(enemyTextAsset.text);
            UnityEngine.Debug.Log($"加载了 {enemyDatas?.Count ?? 0} 个敌人数据");
        }
        else
        {
            UnityEngine.Debug.LogError("无法加载 enemy.json 文件");
        }

    }

    // 用于场景切换后确保单例可用
    public static void EnsureInstanceExists()
    {
        if (Instance == null)
        {
            // 在场景中查找现有的 GameManage
            GameManage existing = FindObjectOfType<GameManage>();
            if (existing != null)
            {
                Instance = existing;
            }
            else
            {
                // 如果不存在，创建一个新的
                GameObject go = new GameObject("GameManager");
                Instance = go.AddComponent<GameManage>();
                DontDestroyOnLoad(go);
            }
        }
    }

    public object RandomOne<T>(List<T> list)
    {
        if(list == null || list.Count == 0)
        {
            return null;
        }
        int index = Random.Range(0, list.Count); 
        return list[index];
    }
}
