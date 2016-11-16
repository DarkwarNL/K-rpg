using UnityEngine;
using System.Collections;
using System;

public class Archer : Combat
{
    public Arrow _SelectedArrow;
    private ArrowSlot _ArrowSlot;
    private CameraFollow _Cam;
    private CharacterMovement _Movement;

    void Start()
    {
        _Weapons = Resources.LoadAll<GameObject>("Prefabs/Weapons/Archer");
        _ArrowSlot = GetComponentInChildren<ArrowSlot>();
        _Cam = CameraFollow.Instance;
        _Movement = GetComponent<CharacterMovement>();
    }

    protected override void Aim()
    {
        if (!_Fighting)
        {
            UnSheath();                      
        }        
        _Anim.SetBool("Aim", true);
        _Cam.Aiming();
        _Movement.Aiming = true;
        _Aiming = true;
        _ArrowSlot.SpawnArrow(_SelectedArrow);
        SetCrosshair();
    }

    private void SetCrosshair()
    {
        RaycastHit hit;
        Vector3 fwd = _Cam.transform.forward;
            
        if (Physics.Raycast(_Cam.transform.position, fwd, out hit, 25))
        {
            Debug.DrawRay(_Cam.transform.position, fwd* 25, Color.red,5);
        }
        _ArrowSlot.SetArrowRotation(hit.point);
    }

    protected override void Shoot()
    {
        for (int i = 0; i < _Anim.layerCount; i++)
        {
            AnimatorStateInfo stateInfo = _Anim.GetCurrentAnimatorStateInfo(i);
            if (stateInfo.IsName("Aim_Movement"))
            {
                _ArrowSlot.Shoot();
                StartCoroutine("Cooldown");
            }
        }
    }

    protected override void Release()
    {
        _ArrowSlot.Release();
        _Aiming = false;
        _Movement.Aiming = false;
        _Anim.SetBool("Aim", false);
        _Cam.Normal();
    }
}