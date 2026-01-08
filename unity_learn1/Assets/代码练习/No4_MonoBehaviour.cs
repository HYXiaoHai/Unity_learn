using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
//所有unity脚本都派生自该类
//
public class No4_MonoBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //monobehaviour派生自组件脚本，因此组件脚本所有的公有，保护的属性，成员变量//
        //Object - Component - Behaviour - MonoBehaviour
        //方法等功能，MonoBehaviour也都有，继承mono之后这类可以挂载到
        Debug.Log("No4_MonoBehaviour组件的激活状态是:" + this.enabled);
        Debug.Log("No4_MonoBehaviour组件的对象名称是:" + this.name);
        Debug.Log("No4_MonoBehaviour组件的对象标签是:" + this.tag);
        Debug.Log("No4_MonoBehaviour组件是否已经激活并且已启用behavior:" + this.isActiveAndEnabled);

    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log("No4_MonoBehaviour组件是否已经激活并且已启用behavior:" + this.isActiveAndEnabled);
    }
}
