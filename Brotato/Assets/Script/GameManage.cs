using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
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
        if (Instance == null)
        Instance = this;

            DontDestroyOnLoad(gameObject);
        enemyTextAsset = Resources.Load<TextAsset>("Data/enemy");
        enemyDatas = JsonConvert.DeserializeObject<List<EnemyData>>(enemyTextAsset.text);


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
