using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//场景管理的空间

//
//
//
public class No21_ScenesManage : MonoBehaviour
{
    AsyncOperation ao;
    // Start is called before the first frame update
    void Start()
    {

        //加载场景序号为1的场景
        //StartCoroutine(LoadScene());

        //普通加载，会导致卡顿
        //加载场景序号为1的场景
        //SceneManager.LoadScene(1);
        //加载场景名字为test1的场景
        //SceneManager.LoadScene("Test1");

        //异步加载（进度条，动画）王者，英雄联盟加载界面
        //SceneManager.LoadSceneAsync("Test1");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //开始加载
            StartCoroutine (LoadNextAsyncScene());
        }
        if (Input.anyKeyDown && ao.progress >= 0.9f)
        {
            ao.allowSceneActivation = true;//允许场景跳转
        }

    }
    IEnumerator LoadNextAsyncScene()
    {
        ao = SceneManager.LoadSceneAsync(1);
        ao.allowSceneActivation = false;//关闭场景自动跳转

        while (ao.progress<0.9f)
        {
            //当前场景加载进入小于0.9
            //当前协程挂起，一直加载
            Debug.Log(ao.progress * 100);
            yield return null;
        }
        Debug.Log("按下任意键继续游戏");
    }
    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(1);

        //加载场景序号为1的场景
        //SceneManager.LoadScene(1);
        //加载场景名字为test1的场景
        SceneManager.LoadScene("Test1");
    }
}
