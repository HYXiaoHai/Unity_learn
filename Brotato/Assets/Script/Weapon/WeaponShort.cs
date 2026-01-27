using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WeaponShort : WeaponBaase
{
    private Vector3 originalLocalPosition; //记录初始本地位置
    private bool isReturning = false; // 标记是否正在返回
    public float moveSpeed;//近战移动速度
    public void Start()
    {
        originalLocalPosition = transform.localPosition;
    }
    public override void Fire()
    {
        if (isCooling|| isReturning)
        {
            return;
        }
        //近战武器
        GetComponent<PolygonCollider2D>().enabled = true;

        //发动攻击
        canAiming = false;
        StartCoroutine(AttackSequence());
        isCooling = true;
    }
    IEnumerator AttackSequence()
    {
        // 阶段1：冲向敌人
        yield return StartCoroutine(MoveToEnemy());

        // 阶段2：返回原位
        yield return StartCoroutine(ReturnToOriginalPosition());

        // 攻击结束，重置状态
        canAiming = true;
    }
    IEnumerator MoveToEnemy()
    {
        if(enemy == null)
        {
            yield break;
        }
        Vector3 targetPosition = enemy.position + new Vector3(0, enemy.GetComponent<SpriteRenderer>().size.y / 2, 0);
        float startTime = Time.time;
        float maxDuration = data.range / moveSpeed * 2f; // 设置最大持续时间，防止卡住
        while(Vector3.Distance(transform.position,targetPosition)>0.1)
        {
            if (enemy == null || enemy.gameObject.activeSelf == false)
            {
                break; // 敌人死亡，立即停止前进
            }
            //更新位置
            targetPosition = enemy.position + new Vector3(0, enemy.GetComponent<SpriteRenderer>().size.y / 2, 0);
            //防止移动时间过长
            if (Time.time - startTime > maxDuration)
                break;
            //计算移动方向
            Vector3 direction = (targetPosition - transform.position).normalized;
            float remainingDistance = Vector3.Distance(transform.position, targetPosition);
            float distanceThisFrame = moveSpeed * Time.deltaTime;
            //防止过冲
            if (distanceThisFrame > remainingDistance)
                distanceThisFrame = remainingDistance;

            transform.position += direction * distanceThisFrame;
            yield return null;
        }
    }
    IEnumerator ReturnToOriginalPosition()
    {
        isReturning = true;
        // 计算在世界空间中的原始位置
        Vector3 worldOriginalPosition = transform.parent.TransformPoint(originalLocalPosition);

        while (Vector3.Distance(transform.position, worldOriginalPosition) > 0.05f)
        {
            // 计算移动方向
            Vector3 direction = (worldOriginalPosition - transform.position).normalized;
            float distanceThisFrame = moveSpeed * Time.deltaTime;
            float remainingDistance = Vector3.Distance(transform.position, worldOriginalPosition);

            // 防止过冲
            if (distanceThisFrame > remainingDistance)
                distanceThisFrame = remainingDistance;

            transform.position += direction * distanceThisFrame;
            yield return null;
        }

        //回到原位
        transform.localPosition = originalLocalPosition;
        //可以瞄准
            canAiming = true;
        isReturning = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyBase>().Injured(data.damage);
            gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        }
    }
}
