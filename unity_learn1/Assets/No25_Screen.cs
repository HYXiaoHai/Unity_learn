using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class No25_Screen : MonoBehaviour
{
    private void Start()
    {
        //当前屏幕(电脑显示器 不是窗口)分辨率
        Resolution r =  Screen.currentResolution;
        Debug.Log("当前屏幕分辨率："+r+"宽"+r.width+"高"+r.height);
       
        //屏幕当前窗口的宽高
        //当前窗口的宽高
        //一边写代码调试的时候
        Debug.Log("宽"+Screen.width+"高"+Screen.height);

        //屏幕休眠模式
        Screen.sleepTimeout = SleepTimeout.NeverSleep;//永远不息屏

        //是否全屏模式
        Screen.fullScreen = true;//全屏
        //窗口模式
        Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;

        Screen.SetResolution(1920,1080,false);//设置分辨率方法

        //屏幕坐标熟悉
    }
}
