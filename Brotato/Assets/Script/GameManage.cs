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
}
