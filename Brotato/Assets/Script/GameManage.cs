using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManage : MonoBehaviour
{
    public static GameManage Instance;
    public RoleData currentRole;
    
    public List<WeaponData> currentWeapon = new List<WeaponData>();//记录当前所有武器

    public DifficutyData currentDifficulty;//

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
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
