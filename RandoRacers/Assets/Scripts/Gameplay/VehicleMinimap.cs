using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMinimap : MonoBehaviour {
    private Transform _Target;
    [SerializeField]
    private float _Height;

    public void SetTarget(Transform target)
    {
        _Target = target;
    }

	void FixedUpdate ()        
    {
        if (!_Target) return;
        Vector3 newPos = _Target.position;
        newPos.y = _Height;
        transform.position = newPos;
    }
}
