using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    internal Transform _Target;
    public LayerMask mask;

    private float _TargetHeight = 1.7f;
    private float _Distance = 10.0f;

    private float _MaxDistance = 10;
    private float _MinDistance = .6f;

    private float _XSpeed = 3f;
    private float _YSpeed = 5f;

    private int _YMinLimit = -65;
    private int _YMaxLimit = 50;

    private int _ZoomAmount = 20;
    private float _ZoomDampening = 5.0f;

    private float _X = 0.0f;
    private float _Y = 0.0f;
    private float _CurrentDistance;
    private float _DesiredDistance;
    private float _CorrectedDistance;
    private static CameraFollow _CameraFollow;

    public static CameraFollow Instance
    {
        get
        {
            if (!_CameraFollow) _CameraFollow = FindObjectOfType<CameraFollow>();
            return _CameraFollow;
        }
    }


    void Start()
    {
        _Target = FindObjectOfType<VehicleController>().transform;

        Vector3 angles = transform.eulerAngles;
        _X = angles.x;
        _Y = angles.y;

        _CurrentDistance = _Distance;
        _DesiredDistance = _Distance;
        _CorrectedDistance = _Distance;
    }

    /**
     * Camera logic on LateUpdate to only update after all character movement logic has been handled.
     */
    void FixedUpdate()
    {
        if (!_Target)
            return;

        // set local camera rotation
        Quaternion rotation = Quaternion.LookRotation(_Target.transform.forward, Vector3.up);

        // calculate desired camera position
        Vector3 position = _Target.position - (rotation * Vector3.forward * _DesiredDistance + new Vector3(0, -_TargetHeight, 0));

        // check for collision using the true target's desired registration point as set by user using height
        RaycastHit collisionHit;
        Vector3 trueTargetPosition = new Vector3(_Target.position.x, _Target.position.y + _TargetHeight, _Target.position.z);

        // if there was a collision, correct the camera position and calculate the corrected distance
        bool isCorrected = false;
        if (Physics.Linecast(trueTargetPosition, position, out collisionHit, mask))
        {
            position = collisionHit.point;
            _CorrectedDistance = Vector3.Distance(trueTargetPosition, position);
            isCorrected = true;
        }

        // For smoothing, lerp distance only if either distance wasn't corrected, or correctedDistance is more than currentDistance
        _CurrentDistance = !isCorrected || _CorrectedDistance > _CurrentDistance ? Mathf.Lerp(_CurrentDistance, _CorrectedDistance, Time.deltaTime * _ZoomDampening) : _CorrectedDistance;

        // recalculate position based on the new currentDistance
        position = _Target.position - (rotation * Vector3.forward * _CurrentDistance + new Vector3(0, -_TargetHeight, 0));

        transform.rotation = rotation;
        transform.position = position;
    }
}