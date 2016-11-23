using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class CharacterMovement : MonoBehaviour {
    private Stats _Stats;
    private Animator _Anim;
    private float _RotationSpeed = 30;
    internal bool Aiming;
    public Transform spine;

    void Awake()
    {
        _Stats = GetComponent<Stats>(); 
        _Anim = GetComponent<Animator>();
    }

    void LateUpdate()
    {
        float moveV = Input.GetAxis("Vertical");
        float moveH = Input.GetAxis("Horizontal");
        _Anim.SetFloat("Speed", moveV);
        _Anim.SetFloat("SideSpeed", moveH);

        if (Input.GetButtonDown("Jump"))
        {
            _Anim.SetTrigger("Jump");
        }
        
        if (Aiming)
        {
            Quaternion newRot = spine.rotation;
            newRot.x = Camera.main.transform.rotation.x;
            spine.rotation = newRot;

            Quaternion targetRotation = Quaternion.LookRotation(Camera.main.transform.transform.forward, Vector3.up);
            Quaternion newRotation = Quaternion.Lerp(transform.rotation, targetRotation, _RotationSpeed * Time.deltaTime);

            newRotation.x = transform.rotation.x;
            newRotation.z = transform.rotation.z;
            transform.rotation = newRotation;
            
        }
        else if(moveV > 0)
        {
            Vector3 movement = new Vector3(0, 0f, moveV);

            movement = Camera.main.transform.TransformDirection(movement);
            Quaternion targetRotation = Quaternion.LookRotation(movement, Vector3.up);
            Quaternion newRotation = Quaternion.Lerp(transform.rotation, targetRotation, _RotationSpeed * Time.deltaTime);

            newRotation.x = transform.rotation.x;
            newRotation.z = transform.rotation.z;
            transform.rotation = newRotation;
        }        
    }
}