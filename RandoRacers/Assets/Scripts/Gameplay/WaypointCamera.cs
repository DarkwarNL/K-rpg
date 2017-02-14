using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointCamera : MonoBehaviour {
    [SerializeField]
    private Transform[] _Waypoints = new Transform[4];

    private AnimationCurve _Curve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
    private Transform _Start;
    private Transform _End;
    private float _Duration = 800f;
    private float _Speed = 1;

    private int _CurrentWaypoint = 0;

    void Awake()
    {
        _Start = transform;
        _End = _Waypoints[_CurrentWaypoint];
    }

	void FixedUpdate ()
    {        
        _Speed += Time.deltaTime;
        float s = _Speed / _Duration;

        if (Vector3.Distance(transform.position, _Waypoints[_CurrentWaypoint].transform.position) < 800)
        {
            transform.rotation = Quaternion.Lerp(_Start.rotation, _End.rotation, _Curve.Evaluate(s));
        }

        if (Vector3.Distance(_Start.position, _End.position) > 100 )
        {
            transform.position = Vector3.Lerp(_Start.position, _End.position, _Curve.Evaluate(s));
        }
        else
        {
            _CurrentWaypoint++;
            if (_CurrentWaypoint == _Waypoints.Length) 
                _CurrentWaypoint = 0;

            _Speed = 1f;
            _End = _Waypoints[_CurrentWaypoint];
        }
    }
}