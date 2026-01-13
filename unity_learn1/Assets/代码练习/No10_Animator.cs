using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//动画
public class No10_Animator : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        //设置值
        animator.SetFloat("speed",0.1f) ;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isrun",false);

        if (Input.GetKey(KeyCode.K))
        {
            //以标准化单位时间进行淡入淡出
            animator.CrossFade("Run",0.5f);//动画融合的时间
            //以秒为单位进行淡入淡出
            animator.CrossFadeInFixedTime("Run", 0.5f);//物理时间融合
            //animator.SetBool("isrun", true);
        }
    }
}
