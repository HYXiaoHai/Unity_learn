using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelectPanel : MonoBehaviour
{
    public static WeaponSelectPanel Instance;
    public Transform _weaponContent;
    public CanvasGroup _canvasGroup;
    private void Awake()
    {
        Instance = this;

        _canvasGroup = GetComponent<CanvasGroup>();
        _weaponContent = GameObject.Find("WeaponContent").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
