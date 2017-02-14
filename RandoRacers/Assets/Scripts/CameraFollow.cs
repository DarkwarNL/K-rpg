using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    internal Transform Target;
    public LayerMask mask;
    
    private float _DesiredDistance = 10.0f;
    private float _DesiredHeight = 3.5f;
    private float _PositionDampening = 25.0f;
    private float _RotationDampening = 5.0f;
    
    private float _CurrentDistance;
    private float _CorrectedDistance;
    private static CameraFollow _CameraFollow;

    void Start()
    {   
        _CurrentDistance = _DesiredDistance;
        _CorrectedDistance = _DesiredDistance;
    }

    /**
     * Camera logic on LateUpdate to only update after all Vehicle movement logic has been handled.
     */
    void LateUpdate()
    {
        if (!Target)
            return;
        // set local camera rotation
        Quaternion rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Target.transform.forward, Vector3.up), Time.deltaTime*_RotationDampening);

        // calculate desired camera position
        Vector3 position = Target.position - (rotation * Vector3.forward * _DesiredDistance + new Vector3(0, -_DesiredHeight, 0));

        // check for collision using the true target's desired registration point as set by user using height
        RaycastHit collisionHit;
        Vector3 trueTargetPosition = new Vector3(Target.position.x, Target.position.y + _DesiredHeight, Target.position.z);

        _CorrectedDistance = Vector3.Distance(trueTargetPosition, position);
        // if there was a collision, correct the camera position and calculate the corrected distance
        bool isCorrected = false;
        if (Physics.Linecast(trueTargetPosition, position, out collisionHit, mask))
        {
            position = collisionHit.point;
            _CorrectedDistance = Vector3.Distance(trueTargetPosition, position);
            isCorrected = true;
        }
        
        // For smoothing, lerp distance only if either distance wasn't corrected, or correctedDistance is not current distance
        _CurrentDistance = !isCorrected || _CorrectedDistance != _CurrentDistance ? Mathf.Lerp(_CurrentDistance, _CorrectedDistance, Time.deltaTime * _PositionDampening) : _CorrectedDistance;

        // recalculate position based on the new currentDistance
        position = Target.position - (rotation * Vector3.forward * _CurrentDistance + new Vector3(0, -_DesiredHeight, 0));

        transform.rotation = rotation;
        transform.position = position;
    }
}