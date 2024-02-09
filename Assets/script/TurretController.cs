using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class TurretController : MonoBehaviour
{
    public GameObject missilePrefab; // 导弹预制体
    public float launchInterval = 3.0f; // 发射间隔

    void Start()
    {
        InvokeRepeating("LaunchMissile", launchInterval, launchInterval);
    }

    void LaunchMissile()
    {
        // 实例化导弹
        Instantiate(missilePrefab, transform.position, Quaternion.identity);
    }
}
