using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    internal Transform _Target;
    public LayerMask mask;

    private float _TargetHeight = 1.7f;
    private float _Distance = 5.0f;

    private float _MaxDistance = 5;
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
    private Vector3 _Add;
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
        _Target = FindObjectOfType<CharacterMovement>().transform;

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
    void Update()
    {
        // Don't do anything if target is not defined
        if (!_Target)
            return;

        // If either mouse buttons are down, let the mouse govern camera position
        //if (Input.GetAxis("RVertical") != 0 || Input.GetAxis("RHorizontal") != 0)
        if(Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            _X += Input.GetAxis("Mouse X") * _YSpeed ;
            _Y -= Input.GetAxis("Mouse Y") * _XSpeed;
        }

        _Y = ClampAngle(_Y, _YMinLimit, _YMaxLimit);

        // set camera rotation
        Quaternion rotation = Quaternion.Euler(_Y, _X, 0);

        // calculate the desired distance
        _DesiredDistance -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * _ZoomAmount * Mathf.Abs(_DesiredDistance);
        _DesiredDistance = Mathf.Clamp(_DesiredDistance, _MinDistance, _MaxDistance);
        _CorrectedDistance = _DesiredDistance;

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
        position += _Add;

        transform.rotation = rotation;
        transform.position = position;
    }

    public void Aiming()
    {
        _TargetHeight = 2f;
        _Distance = 2.5f;
       // _Add = _Target.transform.right / 2;
        _DesiredDistance = _Distance;
        _CorrectedDistance = _Distance;
    }

    public void Normal()
    {
        _TargetHeight = 1.7f;
        _Distance = 5.0f;
        _Add = new Vector3();
        _DesiredDistance = _Distance;
        _CorrectedDistance = _Distance;
    }

    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
}