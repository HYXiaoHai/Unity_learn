using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{
    private Animator animator;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(horizontal,0,vertical).normalized;
        if(dir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(dir);
            animator.SetBool("isWalk",true);
            transform.Translate(Vector3.forward*2*Time.deltaTime);
        }
        else
        {
            animator.SetBool("isWalk", false);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            GetComponent<Animator>().SetTrigger("PickUp");
        }
    }

    void Pickup()
    {
        Debug.Log("捡起");
    }

    //IK事件 逆动力学
    private void OnAnimatorIK(int layerIndex)
    {
        //设置头部
        animator.SetLookAtWeight(1);
        animator.SetLookAtPosition(target.position);
        //设置右手
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand,1);//旋转

        animator.SetIKPosition(AvatarIKGoal.RightHand, target.position);//使用右手IK;
        animator.SetIKRotation(AvatarIKGoal.RightHand,target.rotation);
    }
}
