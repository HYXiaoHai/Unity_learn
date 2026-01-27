using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLong : WeaponBaase
{
    public GameObject bullet;
    public float movespeed;
    public AudioClip fireAudio;

    public override void Awake()
    {
        base.Awake();
        fireAudio = Resources.Load<AudioClip>("Music/Éä»÷ÒôÐ§");

    }
    public override void Fire()
    {
        if (isCooling)
        {
            return;
        }
        //·¢¶¯¹¥»÷
        CreatBullet();
        //StartCoroutine(CreatBullet());
    }

    void CreatBullet()
    {
        pistol_bullet p =  Instantiate( bullet,transform.position,Quaternion.identity).GetComponent<pistol_bullet>();
        audioSource.PlayOneShot(fireAudio);
        Vector3 direction = (enemy.transform.position - transform.position).normalized;
        p.Init(direction,enemy);
        p.damage = data.damage;
        isCooling=true;
    }
}
