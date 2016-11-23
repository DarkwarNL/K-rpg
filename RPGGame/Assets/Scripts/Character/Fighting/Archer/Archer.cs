using UnityEngine;
using System.Collections;
using System;

public class Archer : CombatStyle
{
    public Arrow _SelectedArrow;
    private ArrowSlot _ArrowSlot;    
    private bool _Aiming = false;

    void Start()
    {        
        _ArrowSlot = GetComponentInChildren<ArrowSlot>();
    }

    protected override void Aim()
    {
        _Anim.SetBool("Aim", true);
        _Cam.Aiming();
        _Movement.Aiming = true;        
        _Crosshair.SetActive( true);
        _Aiming = true;
        for (int i = 0; i < _Anim.layerCount; i++)
        {
            AnimatorStateInfo stateInfo = _Anim.GetCurrentAnimatorStateInfo(i);
            if (stateInfo.IsName("Aim_Movement"))
            {
                SetCrosshair();
            }
            else if(stateInfo.IsName("Aim_Start"))
            {
                _ArrowSlot.SpawnArrow(_SelectedArrow, _Weapon.BaseDamage);
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
        if (!_CanAttack) return;
        AnimatorStateInfo stateInfo = _Anim.GetCurrentAnimatorStateInfo(1);
        if (stateInfo.IsName("Aim_Movement"))
        {
            _ArrowSlot.Shoot();
            StartCoroutine(Cooldown(1));
        }        
    }

    protected override void ReleaseAim()
    {
        _Aiming = false;
        _ArrowSlot.Release();
        _Movement.Aiming = false;
        _Anim.SetBool("Aim", false);
        _Cam.Normal();
        _Crosshair.SetActive(false);
    }
    protected override bool IsFighting()
    {
        return _Aiming;
    }

    protected override void ReleaseAttack()
    {
        
    }
}