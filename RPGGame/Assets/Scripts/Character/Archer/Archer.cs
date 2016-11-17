using UnityEngine;
using System.Collections;
using System;

public class Archer : Combat
{
    public Arrow _SelectedArrow;
    private ArrowSlot _ArrowSlot;
    private CameraFollow _Cam;
    private CharacterMovement _Movement;
    private GameObject _Crosshair;
    void Start()
    {
        _Weapons = Resources.LoadAll<GameObject>("Prefabs/Weapons/Archer");
        _ArrowSlot = GetComponentInChildren<ArrowSlot>();
        _Crosshair = GameObject.FindGameObjectWithTag("Crosshair");
        _Cam = CameraFollow.Instance;
        _Movement = GetComponent<CharacterMovement>();
    }

    protected override void Aim()
    {
        _Anim.SetBool("Aim", true);
        _Cam.Aiming();
        _Movement.Aiming = true;        
        _Crosshair.SetActive( true);

        for (int i = 0; i < _Anim.layerCount; i++)
        {
            AnimatorStateInfo stateInfo = _Anim.GetCurrentAnimatorStateInfo(i);
            if (stateInfo.IsName("Aim_Movement"))
            {
                SetCrosshair();
            }
            else
            {
                _ArrowSlot.SpawnArrow(_SelectedArrow, _SelectedWeapon.BaseDamage);
            }
        }
    }

    private void SetCrosshair()
    {
        Vector3 fwd = _Cam.transform.forward;
        _ArrowSlot.SetArrowRotation(_Cam.transform.position +(fwd * 25));        
    }

    protected override void Attack()
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
        _Fighting = false;
        _Movement.Aiming = false;
        _Anim.SetBool("Aim", false);
        _Cam.Normal();
        _Crosshair.SetActive(false);
    }
}