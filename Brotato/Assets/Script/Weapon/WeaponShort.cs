using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponShort : WeaponBaase
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyBase>().Injured(data.damage);
            gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        }
    }
}
