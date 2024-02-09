// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class route : MonoBehaviour
// {
//     [SerializeField]
//     private Transform[] controlPoints;

//     private Vector2 gizmosPosition;

//     private void OnDrawGizmos()
//     {
//         if (controlPoints == null || controlPoints.Length != 4)
//         {
//         // 如果没有足够的控制点，只是返回并不执行任何操作
//             return;
//         }
//         for (float t = 0; t <= 1; t += 0.05f)
//         {
//             gizmosPosition = Mathf.Pow(1 - t, 3) * controlPoints[0].position + 3 * Mathf.Pow(1 - t, 2) * t * controlPoints[1].position + 3 * (1 - t) * Mathf.Pow(t, 2) * controlPoints[2].position + Mathf.Pow(t, 3) * controlPoints[3].position;

//             Gizmos.DrawSphere(gizmosPosition, 0.25f);
//         }

//         Gizmos.DrawLine(new Vector2(controlPoints[0].position.x, controlPoints[0].position.y), new Vector2(controlPoints[1].position.x, controlPoints[1].position.y));
//         Gizmos.DrawLine(new Vector2(controlPoints[2].position.x, controlPoints[2].position.y), new Vector2(controlPoints[3].position.x, controlPoints[3].position.y));

//     }
// }
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Route : MonoBehaviour
{
    [SerializeField]
    private Transform[] controlPoints;

    private Vector3[] positions;

    private void Start()
    {
        DrawCurve();
    }

    private void DrawCurve()
    {
        if (controlPoints == null || controlPoints.Length != 4)
        {
            return;
        }

        List<Vector3> positionsList = new List<Vector3>();
        for (float t = 0; t <= 1; t += 0.05f)
        {
            Vector3 position = Mathf.Pow(1 - t, 3) * controlPoints[0].position +
                               3 * Mathf.Pow(1 - t, 2) * t * controlPoints[1].position +
                               3 * (1 - t) * Mathf.Pow(t, 2) * controlPoints[2].position +
                               Mathf.Pow(t, 3) * controlPoints[3].position;

            positionsList.Add(position);
        }

        positions = positionsList.ToArray();
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = positions.Length;
        lineRenderer.SetPositions(positions);
    }
    [SerializeField]
   // private Transform[] controlPoints;

    private Vector2 gizmosPosition;

    private void OnDrawGizmos()
    {
        for(float t = 0; t <= 1; t += 0.05f)
        {
            gizmosPosition = Mathf.Pow(1 - t, 3) * controlPoints[0].position + 3 * Mathf.Pow(1 - t, 2) * t * controlPoints[1].position + 3 * (1 - t) * Mathf.Pow(t, 2) * controlPoints[2].position + Mathf.Pow(t, 3) * controlPoints[3].position;

            Gizmos.DrawSphere(gizmosPosition, 0.25f);
        }

        Gizmos.DrawLine(new Vector2(controlPoints[0].position.x, controlPoints[0].position.y), new Vector2(controlPoints[1].position.x, controlPoints[1].position.y));
        Gizmos.DrawLine(new Vector2(controlPoints[2].position.x, controlPoints[2].position.y), new Vector2(controlPoints[3].position.x, controlPoints[3].position.y));

    }
}
