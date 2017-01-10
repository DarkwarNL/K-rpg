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
        _SelectedArrow = Resources.Load<Arrow>("Prefabs/Arrows/TeleportArrow");
        SelectedSkills = Resources.LoadAll<Skill>("Prefabs/ArcherParticleAttacks");
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

    protected override void CheckSkill(int i)
    {
        if (SelectedSkills[i].GetComponent<Skill_RainOfArrows>())
        {
            Vector3 fwd = transform.TransformDirection(_Cam.transform.forward);
            LayerMask layerMask = 1 << 0;
            RaycastHit hit;

            if (Physics.Raycast(_Cam.transform.position, fwd, out hit, 500, layerMask))
            {
                GameObject rainOfArrows = Instantiate(SelectedSkills[i].gameObject);
                
                rainOfArrows.GetComponent<Skill_RainOfArrows>().SetPosition(hit.point);

                StartCoroutine(SelectedSkills[i].CastCooldown());

                StartCoroutine(ActionBar.Instance.SetCooldown(i, 5));
            }
        }
        else if (SelectedSkills[i].GetComponent<Skill_MissileArrows>())
        {
            Instantiate(SelectedSkills[i].gameObject, transform.position + transform.up, transform.rotation);

            StartCoroutine(SelectedSkills[i].CastCooldown());
            StartCoroutine(ActionBar.Instance.SetCooldown(i, 5));
        }
        
         
    }

    protected override bool IsFighting()
    {
        if (_Fighting)
        {
            AnimatorStateInfo stateInfo = _Anim.GetCurrentAnimatorStateInfo(2);
            if (stateInfo.IsName("Aim_Start"))
            {
                _ArrowSlot.SpawnArrow(_SelectedArrow, _Weapon.BaseDamage);
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