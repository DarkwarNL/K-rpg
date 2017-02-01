using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointCamera : MonoBehaviour {
    [SerializeField]
    private Transform[] _Waypoints = new Transform[4];
    [SerializeField]
    private float _Speed;

    private int _CurrentWaypoint = 0;

	void FixedUpdate ()
    {
        if (Vector3.Distance(transform.position, _Waypoints[_CurrentWaypoint].transform.position) > 2)
        {
            Transform newPoint = _Waypoints[_CurrentWaypoint];
            transform.position = Vector3.MoveTowards(transform.position, newPoint.position, Time.deltaTime * _Speed);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, newPoint.rotation, Time.deltaTime * _Speed);
        }
        else
        {
            _CurrentWaypoint++;
            if (_CurrentWaypoint == _Waypoints.Length)
                _CurrentWaypoint = 0;
        }
	}
}
