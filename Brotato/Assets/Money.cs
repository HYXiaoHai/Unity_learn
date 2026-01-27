using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Money : MonoBehaviour
{
    public float speed = 8;
    public float attractDelay = 0.5f; // 被吸引前的延迟（防止立即吸附）
    public float maxAttractDistance = 0.5f; // 最大吸引距离
    private bool isAttracting = false; // 是否正在被吸引
    private bool canAttrac = false;
    private void Awake()
    {
        // 金币刚生成时不能立即被吸引
        StartCoroutine(EnableAttraction(attractDelay));
    }
    private void Update()
    {
        if(chackDistance()&& canAttrac&& !isAttracting)
        {
            StartCoroutine(AttractToPlayer());
        }
        // 如果回合结束，开始吸引
        if (LevelController.Instance != null && LevelController.Instance.isOver && !isAttracting)
        {
            StartCoroutine(gameEnd());
        }
    }
    bool chackDistance()
    {
        if(Vector2.Distance(Player.instance.transform.position,transform.position)<=maxAttractDistance)
        {
            return true;
        }
        return false;
    }
    IEnumerator EnableAttraction(float delay)
    {
        yield return new WaitForSeconds(delay);
        canAttrac = true;
    }
    //游戏结束
    IEnumerator gameEnd()
    {
        yield return new WaitForSeconds(1);//延迟1秒吸附
        StartCoroutine(AttractToPlayer());

    }

    IEnumerator AttractToPlayer()
    {
        if (Player.instance == null || isAttracting)
            yield break;
        isAttracting=true;

        // 加速吸引效果
        float currentSpeed = speed;
        float acceleration = 2f; // 加速度
        float maxSpeed = 15f; // 最大速度

        while(Vector2.Distance(transform.position, Player.instance.transform.position) > 0.1f)
        {
            if (Player.instance.isDead)
            {
                // 玩家死亡，停止吸引
                isAttracting = false;
                yield break;
            }
            Vector2 direction = (Player.instance.transform.position - transform.position).normalized;
            

            // 加速效果
            currentSpeed = Mathf.Min(currentSpeed + acceleration * Time.deltaTime, maxSpeed);

            transform.position = Vector2.MoveTowards(
                transform.position,
                Player.instance.transform.position,
                currentSpeed * Time.deltaTime
            );

            yield return null;
        }
    }
}
