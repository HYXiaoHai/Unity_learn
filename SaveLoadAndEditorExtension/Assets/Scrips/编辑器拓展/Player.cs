using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [ContextMenuItem("增加HP", "AddHP")]
    public int startingHealth = 100;
    public float flashSpeed = 10;

    public Color color = Color.white;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddHP()
    {
        startingHealth += 10;
    }

    [ContextMenu("设置颜色")]
    void SetColor() //外部右键脚本
    {
        color = Color.red;
    }
}
