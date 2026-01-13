using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

//
//协程 多线程
//
//延时调用（5s后游戏开始、生成怪物）
//和其他逻辑协同执行（耗时的工作进行异步操作，拎出来加载资源）
public class No15_Corouting : MonoBehaviour
{
    public GameObject gris;
    public int Maxnum = 5;
    public Animator animator;
    private Coroutine coroutineInstance;
    private int num = 0;
    void Start()//自上而下的调用
    {
        //// 不同的启动方式，需要不同的停止方式

        //// 1. 字符串方式启动和停止
        //StartCoroutine("ChangeState");
        //// 停止方式1：字符串方式
        //StopCoroutine("ChangeState");

        //// 2. IEnumerator 引用方式启动和停止
        //IEnumerator ie = ChangeState();
        //StartCoroutine(ie);
        //// 停止方式2：IEnumerator 引用
        //StopCoroutine(ie);

        //// 3. Coroutine 对象方式（推荐）
        //coroutineInstance = StartCoroutine(ChangeState());
        //// 停止方式3：通过 Coroutine 对象
        //StopCoroutine(coroutineInstance);

        ////停止全部协程
        //StopAllCoroutines();

        StartCoroutine("CreateGris");

    }

    void Update()
    {
        
    }
    IEnumerator ChangeState( )
    {
        //暂停几秒 协程挂起
        yield return new WaitForSeconds(2);
        animator.Play("Walk");
        yield return new WaitForSeconds(3);
        animator.Play("Run");

        //等待一帧(一下皆是)
        yield return null;
        yield return 10000000000;

        //在本帧帧末执行以下逻辑
        yield return new WaitForEndOfFrame();
    }

    IEnumerator CreateGris()
    {
        StartCoroutine(setCreatCount(5));//开启第二个协程 要注意协程间因为暂停帧数而造成的影响
        while (true)
        {
            if(num >= Maxnum)
            {
               yield break;
            }
            Instantiate(animator.gameObject);
            yield return null;
            num++;
            yield return new WaitForSeconds(1);
        }
    }
    IEnumerator setCreatCount(int Num)
    {
        //yield return null;
        Maxnum = Num;
        yield return null;

    }
}
