using UnityEngine;
using System.Collections;

public class Floating : MonoBehaviour
{
    private float _Height;
    private float _VerticalSpeed = 1;
    private float _Amplitude = 2.5f;
    private Vector3 _TempPosition;

    void Awake()
    {
        _TempPosition = transform.position;
        _Height = transform.position.y;
    }

    void FixedUpdate()
    {
        _TempPosition.y =  _Height + Mathf.Sin(Time.realtimeSinceStartup * _VerticalSpeed) * _Amplitude;
        transform.position = _TempPosition;
    }
}