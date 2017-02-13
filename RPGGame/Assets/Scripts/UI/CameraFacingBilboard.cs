using UnityEngine;
using System.Collections;

public class CameraFacingBilboard : MonoBehaviour
{
    private Camera m_Camera;

    void Awake()
    {
        m_Camera = FindObjectOfType<Camera>();
    }

    void FixedUpdate()
    {
        transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward,
            m_Camera.transform.rotation * Vector3.up);
    }
}