using UnityEngine;
using System.Collections.Generic;
using System;

public class Archer : CombatStyle
{
    private Arrow _SelectedArrow;
    private ArrowSlot _ArrowSlot;    
    
    void Start()
    {
        _ArrowSlot = GetComponentInChildren<ArrowSlot>();
        _SelectedArrow = Resources.Load<Arrow>("Prefabs/Arrows/Arrow");
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
    }

    protected override void CheckSkill(Skill skill)
    {
        Skill_Archer skillToCast = (Skill_Archer)skill;
        StartCoroutine(skillToCast.CastCooldown());
        StartCoroutine(skillToCast.SetImageCooldown());
        Arrow arrow = skillToCast.Arrow;
        if (arrow != null)
        {
            SpawnSkillArrow(arrow);         
        }
    }

    protected void SpawnSkillArrow(Arrow arrow)
    {
        _Anim.SetTrigger("Break");
        _ArrowSlot.Release();
        _ArrowSlot.SpawnArrow(arrow, _Weapon.GetDamage());
    }

    protected override bool IsFighting()
    {
        if (_Fighting)
        {
            AnimatorStateInfo stateInfo = _Anim.GetCurrentAnimatorStateInfo(2);
            if (stateInfo.IsName("Aim_Start"))
            {
                _ArrowSlot.SpawnArrow(_SelectedArrow, _Weapon.GetDamage());
            }
        }

        return _Fighting;
    }

    protected override void ReleaseAttack()
    {
        AnimatorStateInfo stateInfo = _Anim.GetCurrentAnimatorStateInfo(2);
        if (stateInfo.IsName("Movement"))
        {
            SetCrosshair();
            _ArrowSlot.Shoot();
            StartCoroutine(Cooldown(1));
        }      
    }

    protected override void CancelAttack()
    {
        OnEnd();
        _ArrowSlot.Release();
    }
}