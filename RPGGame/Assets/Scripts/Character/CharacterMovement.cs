using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController), typeof(Animator))]
public class CharacterMovement : MonoBehaviour {
    private Stats _Stats;
    private Animator _Anim;
    private CharacterController _Controller;
    private float _RotationSpeed = 6; 

    void Awake()
    {
        _Stats = GetComponent<Stats>();
        _Controller = GetComponent<CharacterController>();
        _Anim = GetComponent<Animator>();
    }

    void Update()
    {
        float moveV = Input.GetAxis("Vertical");
        float moveH = Input.GetAxis("Horizontal");

        _Anim.SetFloat("Speed", moveV);
        _Anim.SetFloat("SideSpeed", moveH);
        if(moveV > 0)
        {
            Vector3 movement = new Vector3(0, 0f, moveV);

            movement = Camera.main.transform.TransformDirection(movement);
            Quaternion targetRotation = Quaternion.LookRotation(movement, Vector3.up);
            Quaternion newRotation = Quaternion.Lerp(transform.rotation, targetRotation, _RotationSpeed * Time.deltaTime);

            newRotation.x = transform.rotation.x;
            newRotation.z = transform.rotation.z;
            transform.rotation = newRotation;
        }        
                
        if (_Controller.isGrounded)
        {
            if (Input.GetButton("Jump"))
            {

            }
        }

        if (Input.GetMouseButton(1))
        {
            transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
        }
    }
}