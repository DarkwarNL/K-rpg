using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class VehicleController : MonoBehaviour {
    #region PRIVATE_VARIABLES
    private Rigidbody _RB;

    /// <summary>
    /// Set the front wheels first
    /// </summary>
    [SerializeField]
    private WheelCollider[] _WheelColliders = new WheelCollider[WheelAmount];
    /// <summary>
    /// set the front wheels first
    /// </summary>
    [SerializeField]
    private GameObject[] _WheelMeshes = new GameObject[WheelAmount];

    [SerializeField]
    private Vector3 _CentreMassOffset;

    private float _MaxSpeedAmount = 150;
    private float _MaxSteerAmount = 17.5f;
    private float _ForceBrakeAmount;
    private float _MaxWheelTorque = 5000;
    private float _MaxSlipAmount = 2f;
    private float _TractionControl = 1;
    private float _BrakeAmount = 200000;
    private float _ReverseSpeed = 500;
    private float _SteerHelper = .75f;
    /// <summary>
    /// The amount of force to keep the vehicle on the ground
    /// </summary>
    private float _ForceDown = 500;

    private float _CurrentTorque;
    private float _PreviousRotation;
    #endregion

    #region PUBLIC_VARIABLES
    public float CurrentSpeed { get; private set; }
    public CameraFollow FollowingCamera;
    public const int WheelAmount = 4;
    public string Name;
    public bool IsBot = false;
    #endregion

    void Awake()
    {
        //lowering the center mass of the rigid body so the vehicle won't flip over as fast 
        _WheelColliders[0].attachedRigidbody.centerOfMass = _CentreMassOffset;

        _ForceBrakeAmount = float.MaxValue;
        _RB = GetComponent<Rigidbody>();
        _CurrentTorque = _MaxWheelTorque - (_TractionControl * _MaxWheelTorque);
    }

    #region VEHICLE_MOVEMENT_FUNCTIONS

    public void SetVehicleData(float steer, float acceleration, float brake, float forceBrake)
    {
        for (int i = 0; i < WheelAmount; i++)
        {
            Quaternion quat;
            Vector3 position;

            // Check where the wheels are supposed to be located
            _WheelColliders[i].GetWorldPose(out position, out quat);

            // Set the wheels on the correct location
            _WheelMeshes[i].transform.position = position;
            _WheelMeshes[i].transform.rotation = quat;
        }


        //clamp variables
        steer = Mathf.Clamp(steer, -1, 1);
        acceleration = Mathf.Clamp(acceleration, 0, 1);
        brake = -1 * Mathf.Clamp(brake, -1, 0);
        forceBrake = Mathf.Clamp(forceBrake, 0, 1);

        //Steer the front wheels
        float steerAmount = steer * _MaxSteerAmount;
        _WheelColliders[0].steerAngle = steerAmount;
        _WheelColliders[1].steerAngle = steerAmount;
        
        SteerHelper();
        MoveVehicle(acceleration, brake);
        CalculateSpeed();

        //ForceBrake
        if (forceBrake > 0f)
        {
            float brakeTorque = forceBrake * _ForceBrakeAmount;
            _WheelColliders[2].brakeTorque = brakeTorque;
            _WheelColliders[3].brakeTorque = brakeTorque;
        }
        else
        {
            _WheelColliders[2].brakeTorque = 0;
            _WheelColliders[3].brakeTorque = 0;
        }

        //add force down so the car wont flip
        AddForceDown();

        TractionControl();
    }

    private void CalculateSpeed()
    {
        float speed = _RB.velocity.magnitude;

        speed *= 3.6f;
        if (speed > _MaxSpeedAmount)
            _RB.velocity = (_MaxSpeedAmount / 3.6f) * _RB.velocity.normalized;

        CurrentSpeed = speed;
    }

    private void SteerHelper()
    {
        for (int i = 0; i < 4; i++)
        {
            WheelHit wheelhit;
            _WheelColliders[i].GetGroundHit(out wheelhit);
            if (wheelhit.normal == Vector3.zero)
                return; // wheels arent on the ground so dont realign the rigidbody velocity
        }

        // this if is needed to avoid gimbal lock problems that will make the car suddenly shift direction
        if (Mathf.Abs(_PreviousRotation - transform.eulerAngles.y) < 10f)
        {
            var turnadjust = (transform.eulerAngles.y - _PreviousRotation) * _SteerHelper;
            Quaternion velRotation = Quaternion.AngleAxis(turnadjust, Vector3.up);
            _RB.velocity = velRotation * _RB.velocity;
        }
        _PreviousRotation = transform.eulerAngles.y;
    }

    private void AddForceDown()
    {
        _RB.AddForce(-Vector3.up * _ForceDown * _WheelColliders[0].attachedRigidbody.velocity.magnitude);
    }

    private void MoveVehicle(float acceleration, float brake)
    {
        float thrustTorque;

        thrustTorque = acceleration * (_CurrentTorque / 2f);
        _WheelColliders[2].motorTorque = _WheelColliders[3].motorTorque = thrustTorque;        

        for (int i = 0; i < 4; i++)
        {
            if (CurrentSpeed > 5 && Vector3.Angle(transform.forward, _RB.velocity) < 50f)
            {
                _WheelColliders[i].brakeTorque = _BrakeAmount * brake;
            }
            else if (brake > 0)
            {
                _WheelColliders[i].brakeTorque = 0f;
                _WheelColliders[i].motorTorque = -_ReverseSpeed * brake;
            }
        }
    }

    // crude traction control that reduces the power to wheel if the car is wheel spinning too much
    private void TractionControl()
    {
        WheelHit wheelHit;
        
        _WheelColliders[2].GetGroundHit(out wheelHit);
        AdjustTorque(wheelHit.forwardSlip);
        
        _WheelColliders[3].GetGroundHit(out wheelHit);
        AdjustTorque(wheelHit.forwardSlip);                
    }

    private void AdjustTorque(float forwardSlip)
    {
        if (forwardSlip >= _MaxSlipAmount && _CurrentTorque >= 0)
        {
            _CurrentTorque -= 10 * _TractionControl;
        }
        else
        {
            _CurrentTorque += 10 * _TractionControl;
            if (_CurrentTorque > _MaxWheelTorque)
            {
                _CurrentTorque = _MaxWheelTorque;
            }
        }
    }
    #endregion

    public void Boost(float value)
    {
        _MaxSpeedAmount = value;
        _MaxSteerAmount = 10f;
        _WheelColliders[2].motorTorque = _WheelColliders[3].motorTorque = 10 * (_CurrentTorque / 2f);
        StartCoroutine(EndBoostIn(10));
    }

    private IEnumerator EndBoostIn(float time)
    {
        yield return new WaitForSeconds(time);
        _MaxSpeedAmount = 150;
        _MaxSteerAmount = 17.5f;
    }


    public void SetColor(Color32 color)
    {
        foreach(Renderer render in transform.GetComponentsInChildren<Renderer>())
        {
            if(render && !render.name.Contains("WheelMesh"))
            {
                foreach(Material mat in render.materials)
                {
                    if (mat.name == "3 (Instance)") continue;                    
                    mat.color = color;
                }
            }
        }
        GetComponentInChildren<VehicleMinimap>().SetTarget(transform);
    }

    public void OnDead()
    {
        _RB.Sleep();
        RaceManager.Instance.GetLastWaypointByName(Name).Respawn(this);
        FollowingCamera.Target = transform;
        _RB.WakeUp();
    }
}