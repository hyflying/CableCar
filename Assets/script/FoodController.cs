using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour
{
  public Vector3 pointA;
    public Vector3 pointB;
    public float speed = 1.0f;

    private float t = 0;
    private int direction = 1;

    void Update()
    {
        // 更新插值参数
        t += direction * speed * Time.deltaTime;

        // 在两点之间来回移动
        transform.position = Vector3.Lerp(pointA, pointB, t);

        // 反转方向
        if (t >= 1f || t <= 0f)
        {
            direction *= -1;
            t = Mathf.Clamp(t, 0f, 1f);
        }
    }
}
