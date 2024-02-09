using UnityEngine;

public class DrawQuarterCircleArc : MonoBehaviour
{
    public int segments = 100; // 圆弧的细分数量
    public float radius = 5f; // 圆弧的半径
    public float startAngle = 180f; // 圆弧开始的角度
    private float arcLength = 270f; // 圆弧的长度，四分之一圆弧为90度
    [ExecuteInEditMode]
    // void Start()
    // {
    //     LineRenderer lineRenderer = GetComponent<LineRenderer>();
    //     lineRenderer.useWorldSpace = false;
    //     lineRenderer.positionCount = segments + 1;

    //     DrawArc(lineRenderer);
    // }
    void Awake() // 或者使用 OnEnable
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;
        lineRenderer.positionCount = segments + 1;

        DrawArc(lineRenderer);
    }


    void DrawArc(LineRenderer lineRenderer)
    {
        float angleStep = arcLength / segments;
        float angle = startAngle;

        for (int i = 0; i <= segments; i++)
        {
            float x = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
            float y = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;
            lineRenderer.SetPosition(i, new Vector3(x, y, 0));
            angle += angleStep;
        }
    }
}
