using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon4 : WeaponLong
{
    public override void Awake()
    {
        bullet = Resources.Load<GameObject>("Prefabs/Pistol_bullet");
        base.Awake();
    }
}
