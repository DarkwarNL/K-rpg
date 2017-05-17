using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public enum Side
{
    Left,
    Right,
    Front,
    Back
}

[RequireComponent(typeof(Animator))]
public class CharacterMovement : MonoBehaviour
{
    private Animator _Anim;
    private PlayerStats _Stats;

    private float _RotationSpeed = 10;
    private float _Speed;
    private float _SideSpeed;

    private float ForceUp = 700;
    private float ForceForward = 100;
    private CapsuleCollider _Collider;
    private const int LerpValue = 5;

    internal bool Aiming;

    #region Sounds
    private AudioSource _Source;
    private AudioClip[] _DirtFootstepClips;
    private AudioClip[] _WaterFootstepClips;

    internal bool CanJump = false;

    protected const string AnimMovementString = "Movement";
    protected const string AnimRollString = "Roll";
    protected const string AnimJumpString = "Jump";

    internal void PlayFootstep()
    {
        _Source.PlayOneShot(_DirtFootstepClips[Random.Range(0, _DirtFootstepClips.Length)]);
    }

    internal void PlaySound(string location)
    {
        _Source.PlayOneShot(Resources.Load<AudioClip>(location));
    }

    internal void HitSound()
    {
        _Source.PlayOneShot(Resources.Load<AudioClip>("Sounds/Damage/hit0" + Random.Range(0, 4)));
    }


    public void SetStats(PlayerStats stats)
    {
        _Stats = stats;
    }

    #endregion

    void Awake()
    {
        _Anim = GetComponent<Animator>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _Collider = GetComponent<CapsuleCollider>();

        _Source = GetComponent<AudioSource>();
        _DirtFootstepClips = Resources.LoadAll<AudioClip>("Sounds/Footsteps/Dirt");
        _WaterFootstepClips = Resources.LoadAll<AudioClip>("Sounds/Footsteps/Water");
    }

    public void Idle()
    {
        _Anim.SetFloat("Idle", Random.Range(0, 2));
    }

    void LateUpdate()
    {
        float moveV = Input.GetAxis("Vertical");
        float moveH = Input.GetAxis("Horizontal");
        float newSpeed = 0;
        float maxClamp = 1;

        AnimatorStateInfo stateInfo = _Anim.GetCurrentAnimatorStateInfo(0);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveV = moveV * (2f + _Stats.MovementSpeed);
            moveH = moveH * (2f + _Stats.MovementSpeed);

            maxClamp = 3;
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt) && !stateInfo.IsName(AnimRollString))
        {
            _Anim.SetTrigger(AnimRollString);
            SetLookAtDirection(new Vector3(moveH, 0f, moveV));
        }
        else
        {
            _Anim.SetBool(AnimRollString, false);
        }

        if (Input.GetButtonDown(AnimJumpString) && CanJump)
        {
            if (_Speed >= 0.8f)
            {
                CanJump = false;
                _Anim.SetBool(AnimJumpString, true);
                _Anim.SetFloat("JumpType", Random.Range(0, 3));
            }
        }

        Ray ray = new Ray(transform.position + Vector3.up, -Vector3.up);
        RaycastHit hitInfo = new RaycastHit();

        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.distance > 2)
            {
                _Anim.SetBool("Falling", true);
            }
            else
            {
                _Anim.SetBool("Falling", false);
            }
        }

        if (moveH != 0 || moveV != 0)
        {
            SetLookAtDirection(new Vector3(moveH, 0f, moveV));

            if (moveV < 0) moveV = -moveV;
            if (moveH < 0) moveH = -moveH;

            newSpeed = Mathf.Clamp(moveV + moveH, 0, maxClamp);
        }

        _Speed = Mathf.Lerp(_Speed, newSpeed, Time.deltaTime * LerpValue);

        if (_Speed <= 0.01f || _Speed >= 3)
        {
            _Speed = 0;
        }

        //_Anim.SetFloat("SideSpeed", _SideSpeed);
        _Anim.SetFloat("Speed", _Speed);
    }

    void SetLookAtDirection(Vector3 movement)
    {
        movement = Camera.main.transform.TransformDirection(movement);
        Quaternion targetRotation = Quaternion.LookRotation(movement, Vector3.up);
        Quaternion newRotation = Quaternion.Lerp(transform.rotation, targetRotation, _RotationSpeed * Time.deltaTime);

        newRotation.x = transform.rotation.x;
        newRotation.z = transform.rotation.z;
        transform.rotation = newRotation;
    }

    public void GotHit(Side side)
    {
        _Anim.SetFloat("HitFrom", (float)side);

        _Anim.SetTrigger("Hit");
    }

    public void AddForce()
    {
        _Anim.SetTrigger("Backflip");

        _Anim.applyRootMotion = false;
        GetComponent<Rigidbody>().AddForce(transform.up * ForceUp);
        GetComponent<Rigidbody>().AddForce(transform.forward * ForceForward);
    }

    private void FixRoot()
    {
        _Anim.applyRootMotion = true;
    }

    public void Dead()
    {
        _Anim.SetBool("Dead", true);

        Invoke("Respawn", 3);
    }

    private void Respawn()
    {
        _Anim.SetBool("Dead", false);

    }

    public void Stop()
    {
        _Speed = 0;
        _Anim.SetFloat("Speed", _Speed);
        this.enabled = false;
    }
}