using System.Collections;
using UnityEngine;

public class CubeRotation : MonoBehaviour
{
    public float rotationSpeed = 90f; // 旋转速度，单位：度/秒
    private bool isRotating = true;

    void Update()
    {
        if (isRotating)
        {
            // 持续旋转立方体
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime); // 绕Y轴旋转
        }
    }

    // 停止旋转
    public void StopRotation()
    {
        isRotating = false;
    }
}

