using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController player;
    private float speed = 2f;
    private bool issgift = false;
    private void Start()
    {
        player = GetComponent<CharacterController>();
    }
    private void Update()
    {
        //水平轴
        float horizontal = Input.GetAxis("Horizontal");
        //垂直轴
        float vertical = Input.GetAxis("Vertical");
        //创建成一个方向向量
        Vector3 dir = new Vector3(horizontal, 0, vertical).normalized;
        Debug.DrawRay(transform.position, dir, Color.red);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            issgift = true;
        }
        else
        {
            issgift = false;
        }
        //移动
        if (issgift == true)
        {
            player.SimpleMove(dir * speed*2);

        }
        else
        {
            player.SimpleMove(dir * speed);
        }
    }
}
