using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class No19_Collider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Collider2D collider = GetComponent<Collider2D>();
    }


    void Update()
    {
        
    }

    //碰撞检测（撞击）轻轻碰则不算
    //最好其中一方有刚体，双方都要有碰撞器
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("碰到了："+collision.gameObject.name);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("在碰撞器里" + collision.gameObject.name);

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("从碰撞器里移出" + collision.gameObject.name);

    }


    //触发检测(碰到就算) 需要一方是触发器。
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("碰到了：" + collision.gameObject.name);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("在触发器里"+collision.gameObject.name);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("从触发器里移出"+collision.gameObject.name);
    }
}
