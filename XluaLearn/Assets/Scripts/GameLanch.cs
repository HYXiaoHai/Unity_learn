using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameLanch : UnitySingleTon<GameLanch>
{
    //初始化框架
    public override void Awake()
    {
        base.Awake();
        //初始化游戏框架 ：lua资源 声音管理 资源管理 
        gameObject.AddComponent<xLuaMgr>();
        //end

    }

    IEnumerator checkHotUpdate()
    {
        //更新资源 更新脚本 
        yield return 0;
    }

    IEnumerator GameStart()
    {
        yield return this.StartCoroutine(this.checkHotUpdate());

        //进入游戏 lua虚拟机 进入lua的逻辑代码 跑起来
        xLuaMgr.Instance.EnterGame();
        Debug.Log("GameStart");
        //end
    }

    // Start is called before the first frame update
    void Start()
    {
        //热更新我们的资源+代码
        StartCoroutine(GameStart());
        //end

        //进入我们的游戏

        //end
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
