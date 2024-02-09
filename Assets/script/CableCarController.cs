using System.Collections;
using UnityEngine;

public class CableCarController : MonoBehaviour
{
    [SerializeField]
    private Transform[] routes;

    private int routeToGo;

    private float tParam;

    private Vector2 objectPosition;

    private float speedModifier;

    // Start is called before the first frame update
    void Start()
    {
        routeToGo = 0;
        tParam = 0f;
        speedModifier = 0.5f; // Adjust speed as needed
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            MoveAlongTheRoute(1); // Move forward
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            MoveAlongTheRoute(-1); // Move backward
        }
    }

    private void MoveAlongTheRoute(int direction)
    {
        // Calculate the new tParam based on the direction
        tParam += Time.deltaTime * speedModifier * direction;

        // Clamp tParam to be between 0 and 1
       // tParam = Mathf.Clamp01(tParam);
        if (tParam > 1f)
        {
            tParam = 0; // Reset tParam
            routeToGo = (routeToGo + 1) % routes.Length; // Move to the next route
        }
        else if (tParam < 0)
        {
            tParam = 1; // Set tParam to the end of the next route
            routeToGo = (routeToGo - 1 + routes.Length) % routes.Length; // Move to the previous route, ensuring index stays valid
        }
        // Get the current route points
        Vector2 p0 = routes[routeToGo].GetChild(0).position;
        Vector2 p1 = routes[routeToGo].GetChild(1).position;
        Vector2 p2 = routes[routeToGo].GetChild(2).position;
        Vector2 p3 = routes[routeToGo].GetChild(3).position;

        // Calculate the Bezier curve point
        objectPosition = Mathf.Pow(1 - tParam, 3) * p0 +
                         3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
                         3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
                         Mathf.Pow(tParam, 3) * p3;

        // Move the cable car to the new position
        transform.position = objectPosition;
    }
}
