using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
//物理系统 刚体
//
//
public class No17_Rigibody : MonoBehaviour
{
    public Rigidbody2D rb;
    private float movespeed = 1f;
    private float angle = 20f;
    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        Debug.Log(rb.position);
        rb.gravityScale = 0;
        Debug.Log("重力影响的程度"+rb.gravityScale);
        rb.mass = 1f;//修改重量

        //施加力（方向+大小，施加力的模式）
        //rb.AddForce(Vector2.right*5,ForceMode2D.Force);//持续不断地力
        //rb.AddForce(Vector2.right*5,ForceMode2D.Impulse);//瞬时   

        rb.velocity = Vector2.right*movespeed;
    }

    // Update is called once per frame
    void Update()
    {
        //rb.MovePosition(rb.position+Vector2.right*movespeed*Time.deltaTime);
    }
    private void FixedUpdate()//物理系统最好放在FixeUpdate
    {
        //移动
        //rb.MovePosition(rb.position+Vector2.right*movespeed*Time.fixedDeltaTime);
        //旋转            角度
        rb.MoveRotation(rb.rotation+angle*Time.fixedDeltaTime);

    }
}
