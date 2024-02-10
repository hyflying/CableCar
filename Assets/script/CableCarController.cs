using System.Collections;
using UnityEngine;

public class CableCarController : MonoBehaviour
{
    [SerializeField]
    private Transform[] routes;
    public Transform[] baskets;

    private int routeToGo;
    private float tParam;
    private Vector2 objectPosition;
    private float speedModifier = 0.5f;
    private float gravityEffect = 1.0f; // 增强重力影响速度的程度
    private bool isMoving = false;

    void Start()
    {
        if (baskets == null || baskets.Length == 0)
        {
            baskets = new Transform[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                baskets[i] = transform.GetChild(i);
            }
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            isMoving = true;
            int direction = Input.GetKey(KeyCode.UpArrow) ? 1 : -1;
            MoveAlongTheRoute(direction);
        }
        else if (isMoving)
        {
            // 当停止输入时，启动自动下滑逻辑
            AutoSlideDueToGravity();
        }
    }

    private void AutoSlideDueToGravity()
    {
        // 根据当前和下一个位置的Y坐标差来判断是否应该自动下滑
        float nextTParam = tParam + (0.01f * Mathf.Sign(gravityEffect));
        Vector3 nextPosition = CalculateBezierPoint(nextTParam);
        Vector3 currentPosition = CalculateBezierPoint(tParam);

        // 如果缆车正在上升段，则减速；如果在下降段，则加速
        float speedAdjustment = nextPosition.y < currentPosition.y ? gravityEffect : -gravityEffect;
        MoveAlongTheRoute(speedAdjustment > 0 ? 1 : -1);
    }
    private void MoveAlongTheRoute(int direction)
{
    // 根据方向和速度调整tParam
    tParam += Time.deltaTime * speedModifier * direction;

    // 向前移动到下一个路线
    if (tParam > 1f)
    {
        if (routeToGo < routes.Length - 1) // 检查是否不是最后一个路线
        {
            tParam = 0; // 重置tParam以在下一个路线的开始处
            routeToGo++; // 移动到下一个路线
            isMoving = true; // 确保继续移动
        }
        else
        {
            // 如果是最后一个路线，停止在最末尾
            tParam = 1f;
            isMoving = false; // 停止移动
        }
    }
    // 向后移动到上一个路线
    else if (tParam < 0)
    {
        if (routeToGo > 0) // 检查是否不是第一个路线
        {
            routeToGo--; // 移动到上一个路线
            tParam = 1f; // 重置tParam以在上一个路线的结束处
            isMoving = true; // 确保继续移动
        }
        else
        {
            // 如果是第一个路线，停止在最开始
            tParam = 0f;
            isMoving = false; // 停止移动
        }
    }

    // 仅当缆车处于移动状态时更新位置
    if (isMoving)
    {
        Vector3 newPosition = CalculateBezierPoint(tParam);
        transform.position = newPosition;
    }
}

    private Vector3 CalculateBezierPoint(float t)
    {
        Vector3 p0 = routes[routeToGo].GetChild(0).position;
        Vector3 p1 = routes[routeToGo].GetChild(1).position;
        Vector3 p2 = routes[routeToGo].GetChild(2).position;
        Vector3 p3 = routes[routeToGo].GetChild(3).position;

        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 p = uuu * p0;
        p += 3 * uu * t * p1;
        p += 3 * u * tt * p2;
        p += ttt * p3;

        return p;
    }

        private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Missile"))
        {
            DropOneBasket();
            Destroy(other.gameObject); // 销毁导弹
        }
        if (other.gameObject.CompareTag("Food"))
        {
            ColorBasket(Color.yellow); // 假设Color.gold是你想要的颜色，如果没有这个颜色，请使用Color.yellow或自定义颜色
            Destroy(other.gameObject); // 可选：销毁food对象
        }
    }
    void DropOneBasket()
    {
        foreach (var basket in baskets)
        {
            Rigidbody2D rb = basket.GetComponent<Rigidbody2D>();
            if (rb != null && rb.isKinematic)
            {
                BasketState basketState = basket.GetComponent<BasketState>();
                basketState.IsDroped = true;
                // 使第一个找到的还未掉落的篮子掉落
                rb.isKinematic = false;
                rb.gravityScale = 1; // 确保重力影响该物体
                break; // 只掉落一个篮子
            }
        }
    }
    void ColorBasket(Color color)
    {
        foreach (var basket in baskets)
        {
            if (basket != null && basket.gameObject.activeInHierarchy)
            {
                BasketState basketState = basket.GetComponent<BasketState>();
                if (basketState != null && !basketState.IsColored && !basketState.IsDroped)
                {
                    var renderer = basket.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        renderer.material.color = color;
                        basketState.IsColored = true; 
                        break; 
                    }
                }
            }
        }
    }
}
