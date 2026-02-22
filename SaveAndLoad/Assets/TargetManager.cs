using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public GameObject[] targets;
    public GameObject currenttarget;

    public int targetPosition;

    private void Start()
    {
        foreach (var item in targets)
        {
            item.GetComponent<BoxCollider>().enabled = false;
            item.SetActive(false);
        }
        StartCoroutine(AliverTimer());
    }

    public void ActivateMonster()
    {
        int index = Random.Range(0, targets.Length);
        currenttarget = targets[index];
        currenttarget.SetActive(true);
        currenttarget.GetComponent<BoxCollider>().enabled = true;
        StartCoroutine(DeathTimer());
    }

    IEnumerator AliverTimer()
    {
        //等待1-4秒
        yield return new WaitForSeconds(Random.Range(1,5));
        ActivateMonster();
    }

    //是激活状态变为未激活
    public void DeActiveMonster()
    {
        if(currenttarget!=null)
        {
            currenttarget.GetComponent<BoxCollider>().enabled=false;
            currenttarget.SetActive(false);
            currenttarget = null;
        }
        StartCoroutine(AliverTimer());
    }
    //与AliverTimer同时开始
    IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(Random.Range(3,8));
        DeActiveMonster();
    }
    //子弹击中怪物
    //重新开始游戏的时候
    public void UpadateMonster()
    {
        StopAllCoroutines();
        if (currenttarget != null)
        {
            currenttarget.GetComponent<BoxCollider>().enabled = false;
            currenttarget.SetActive(false);
            currenttarget = null;
        }
        StartCoroutine(AliverTimer());
    }
    //按照给定的怪物类型激活怪物
    //停止协程
    //将当前激活状态的怪物 转变为激活状态
    //激活给定类型的怪物
    //
    public void ActivateMonsterByType(int type)
    {
        StopAllCoroutines();
        if (currenttarget != null)
        {
            currenttarget.GetComponent<BoxCollider>().enabled = false;
            currenttarget.SetActive(false);
            currenttarget = null;
        }
        currenttarget = targets[type];
        currenttarget.SetActive(true);
        currenttarget.GetComponent<BoxCollider>().enabled = true;
        StartCoroutine(DeathTimer());
    }

}
