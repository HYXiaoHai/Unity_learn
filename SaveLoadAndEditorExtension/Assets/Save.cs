using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Save
{
    public List<int>livingTargetPositions = new List<int>();
    public List<int>livingMonsterTypes = new List<int>();

    public int shootNum = 0;
    public int score = 0;


}
