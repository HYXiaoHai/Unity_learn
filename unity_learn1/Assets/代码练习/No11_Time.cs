using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
//获取时间的类
//

public class No11_Time : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("完成上一帧所用的时间（s）："+Time.deltaTime);
        //Debug.Log("执行物理或其他固定帧率更新的时间间隔（s）："+Time.fixedDeltaTime);
        //Debug.Log("游戏总体时间，固定帧率更新的总时间（s）："+Time.fixedTime);
        Debug.Log("游戏开始以来的总时间（s）："+Time.time);
        //Debug.Log("游戏开始以来的实际时间（s）："+Time.realtimeSinceStartup);
        //Debug.Log("经过平滑处理的Time.deltaTime（s）：" + Time.smoothDeltaTime);
        
        Debug.Log("时间流逝的标度（s）：" + Time.timeScale);//慢放,默认值是1 当变成0.5时,游戏会慢放
        Debug.Log("开启本关卡的到当前的时间（s）：" + Time.timeSinceLevelLoad);
    }
}
