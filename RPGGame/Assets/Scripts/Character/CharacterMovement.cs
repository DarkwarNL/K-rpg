﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class CharacterMovement : MonoBehaviour {
    private Animator _Anim;
    private float _RotationSpeed = 30;
    internal bool Aiming;
    public Transform spine;

    void Awake()
    {
        _Anim = GetComponent<Animator>();
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        float moveV = Input.GetAxis("Vertical");
        float moveH = Input.GetAxis("Horizontal");
        _Anim.SetFloat("Speed", moveV);
        _Anim.SetFloat("SideSpeed", moveH);

        if (moveH >= 0.1f)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * 100.0f * moveH);

        }
        else if (moveH <= -0.1f)
        {
            transform.Rotate(Vector3.down * Time.deltaTime * 200.0f * -moveH);
        }

        if (Input.GetButtonDown("Jump"))
        {
            AnimatorStateInfo stateInfo = _Anim.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName("Movement"))
            {
                _Anim.SetTrigger("Jump");
            }           
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
        else if (moveV != 0 || moveH != 0)
        {
            Vector3 movement = new Vector3(moveH, 0f, moveV);

            movement = Camera.main.transform.TransformDirection(movement);
            Quaternion targetRotation = Quaternion.LookRotation(movement, Vector3.up);
            Quaternion newRotation = Quaternion.Lerp(transform.rotation, targetRotation, _RotationSpeed * Time.deltaTime);

            newRotation.x = transform.rotation.x;
            newRotation.z = transform.rotation.z;
            transform.rotation = newRotation;

            if (moveV < 0) moveV = -moveV;
            if (moveH < 0) moveH = -moveH;
            _Anim.SetFloat("Speed", moveV + moveH);
        }
    }
}


        