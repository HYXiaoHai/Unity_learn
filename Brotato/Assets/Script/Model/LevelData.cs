using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelData
{
    public int id;//关卡
    public int waveTimer;//当前关卡的时间
    public List<WaveData> enemys;//生成敌人信息
}

public class WaveData
{
    public string enemyName;
    public int timeAxis;
    public int count;
    public int elite;
}
