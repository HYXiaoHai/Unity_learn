using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class No13_Random : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //静态变量
        Debug.Log("随机出的旋转数(以四元数的形式)" + Random.rotation);
        Debug.Log("四元数转换欧拉角" + Random.rotation.eulerAngles);
        Debug.Log("随机出[0,1]的随机数" + Random.value);
        Debug.Log("以半径为1的圆中（-1，-1）~（1，1）随机生成一点vector2类型" + Random.insideUnitCircle * 1);//子弹准心射击

        //静态函数
        Debug.Log("在[0,4）范围内生成随机数（整形重载包含Min，不包含Max）" + Random.Range(0, 4));
        Debug.Log("在[0,4]范围内生成随机数（浮点形重载包含Min，包含Max）" + Random.Range(0, 4f));

        Random.InitState(1);//随机种子
        Debug.Log("在[0,4）范围内生成随机数（整形重载包含Min，不包含Max）" + Random.Range(0, 4));

    }

    // Update is called once per frame
    void Update()
    {

    }
}
