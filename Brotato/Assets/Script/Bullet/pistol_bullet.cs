using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class pistol_bullet : MonoBehaviour
{
    public float speed = 10; //子弹速度
    public Vector3 direction;//子弹方向
    public Transform _enemy;//目标
    public Vector3 currentDirect;//当前方向
    public float damage = 1;
    public Transform _map; // 地图
    public float originz;

    //地图
    private float minX;
    private float maxX;
    private float minY;
    private float maxY;
    private Vector2 mapBounds;

    private void Awake()
    {
        // 更安全的方式获取地图
        GameObject mapObj = GameObject.Find("Map");
        if (mapObj != null)
        {
            _map = mapObj.transform;
        }
        originz = transform.eulerAngles.z;

    }

    public void Init(Vector3 dir,Transform pos)
    {
        _enemy = pos;
        direction = dir.normalized;
        UpdateMapBounds();
    }

    private void Start()
    {
        if (_map == null)
        {
            _map = GameObject.Find("Map")?.transform;
        }

        Vector3 direction = _enemy.position - transform.position;
        float angleDegrees = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;//弧度转角度
                                                                                   //Debug.Log(angleDegrees);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angleDegrees + originz);
        UpdateMapBounds();
    }

    private void UpdateMapBounds()
    {
        if (_map != null)
        {
            SpriteRenderer mapRenderer = _map.GetComponent<SpriteRenderer>();
            if (mapRenderer != null)
            {
                Bounds bounds = mapRenderer.bounds;
                minX = bounds.min.x;
                maxX = bounds.max.x;
                minY = bounds.min.y;
                maxY = bounds.max.y;

            }
        }
    }

    private void Update()
    {
        if (direction == Vector3.zero)
        {
            return;
        }

        // 使用Translate沿直线移动
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        // 超出边界自动销毁
        CheckOutOfBounds();
    }

    // 检查是否超出边界
    void CheckOutOfBounds()
    {
        Vector3 position = transform.position;

        // 正确的边界检测
        bool isOutOfBounds = position.x < minX - 0.5f ||
                            position.x > maxX + 0.5f ||
                            position.y < minY - 0.5f ||
                            position.y > maxY + 0.5f;

        if (isOutOfBounds)
        {
            Debug.Log($"子弹超出边界: {position}");
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyBase>().Injured(damage);
            Destroy(gameObject);
        }
    }

}
