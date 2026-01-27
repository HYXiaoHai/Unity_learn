using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon3 : WeaponLong
{
    public override void Awake()
    {
        bullet = Resources.Load<GameObject>("Prefabs/Medical_bullet");
        base.Awake();
    }
}
