using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon5 : WeaponLong
{

    public override void Awake()
    {
        bullet = Resources.Load<GameObject>("Prefabs/Arrow");
        base.Awake();
    }
}
