using UnityEngine;
using System.Collections;
using System;

public class Archer : CombatStyle
{
    public Arrow _SelectedArrow;
    private ArrowSlot _ArrowSlot;    

    void Start()
    {        
        _ArrowSlot = GetComponentInChildren<ArrowSlot>();
    }

    private void SetCrosshair()
    {
        Vector3 fwd = _Cam.transform.forward;
        _ArrowSlot.SetArrowRotation(_Cam.transform.position +(fwd * 25));        
    }

    protected override void Attack()
    {
        OnStart();

        _Movement.Aiming = true;
        _Cam.Aiming();
        SetCrosshair();

        AnimatorStateInfo stateInfo = _Anim.GetCurrentAnimatorStateInfo(2);

        if (stateInfo.IsName("Aim_Start"))
        {
            _ArrowSlot.SpawnArrow(_SelectedArrow, _Weapon.BaseDamage);
        }
       
        if (!_CanAttack) return;

        if (stateInfo.IsName("Movement"))
        {
            _ArrowSlot.Shoot();
            StartCoroutine(Cooldown(1));
        }        
    }

    protected override bool IsFighting()
    {
        return _Fighting;
    }

    protected override void ReleaseAttack()
    {
        OnEnd();
        _ArrowSlot.Release();        
    }
}