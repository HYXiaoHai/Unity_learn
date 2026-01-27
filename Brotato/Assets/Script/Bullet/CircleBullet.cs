using UnityEngine;

public class CircleBullet : MonoBehaviour
{
    public float damage = 1;           // 伤害
    public float rotationSpeed = 30f;  // 旋转速度（度/秒）
    public float radius;               // 旋转半径
    public float currentAngle;         // 当前角度（弧度）

    void Start()
    {
        // 初始位置已经设置好了，现在计算初始角度
        // 根据局部位置计算初始角度
        Vector3 localPos = transform.localPosition;
        if (localPos.magnitude > 0.001f)
        {
            currentAngle = Mathf.Atan2(localPos.y, localPos.x);
        }
    }

    public void Init(float r, Vector3 startdic)
    {
        radius = r;

        // 因为是子物体，使用localPosition
        Vector3 localPos = startdic.normalized * radius;
        transform.localPosition = localPos;

        Debug.Log($"子弹初始化: 半径={radius}, 方向={startdic.normalized}");
    }

    void UpdatePosition()
    {
        // 使用圆周运动公式计算位置
        float x = radius * Mathf.Cos(currentAngle);
        float y = radius * Mathf.Sin(currentAngle);

        // 更新局部位置
        transform.localPosition = new Vector3(x, y, 0);
    }

    void Update()
    {
        // 更新角度
        currentAngle += rotationSpeed * Mathf.Deg2Rad * Time.deltaTime;

        // 更新位置
        UpdatePosition();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Player.instance != null)
        {
            Player.instance.Injured(damage);
        }
    }
}