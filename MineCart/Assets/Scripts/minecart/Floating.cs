using UnityEngine;
using System.Collections;

public class Floating : MonoBehaviour
{
    public float height = 2.5f;
    public float verticalSpeed = 1;
    public float amplitude = 0.19f;
    private Vector3 tempPosition;

    void Start()
    {
        tempPosition = transform.position;
        height = transform.position.y;
    }

    void Update()
    {
        tempPosition.y = Mathf.Sin(Time.realtimeSinceStartup * verticalSpeed) * amplitude;
        transform.position = tempPosition + (transform.up * height);
    }
}