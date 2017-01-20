using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Combat))]
abstract public class CombatStyle : MonoBehaviour {
    protected KeyCode[] keys = new KeyCode[] { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4};
    protected ActionBar _ActionBar;
    protected CameraFollow _Cam;
    protected CharacterMovement _Movement;
    protected Animator _Anim;
    protected Weapon _Weapon;
    protected GameObject _Crosshair;
    protected bool _CanAttack = true;
    protected bool _Fighting;

    /// <summary>
    /// Skills
    /// </summary>
    private SkillDatabase _SkillDatabase;
    
    void Awake()
    {
        _ActionBar = ActionBar.Instance;
        _Cam = CameraFollow.Instance;
        _Movement = GetComponent<CharacterMovement>();

        _Crosshair = GameObject.FindGameObjectWithTag("Crosshair");
        _SkillDatabase = SkillDatabase.Instance;
    }
    
    void FixedUpdate()
    {
        if (!_SkillDatabase) return;
        if (_SkillDatabase.GetSelectedSkills().Length <= 0) return;

        for (int i = 0; i < keys.Length; i++)
        {
            if (Input.GetKeyDown(keys[i]) && _SkillDatabase.GetSelectedSkills()[i].CanCast())
            {
                CheckSkill(_SkillDatabase.GetSelectedSkills()[i]);                
            }
        }
    }

    internal void SetDelegates(Combat combat, Animator anim, Weapon weapon)
    {
        _Anim = anim;
        _Weapon = weapon;
        combat.AttackDelegate = Attack;
        combat.IsFightingDelegate = IsFighting;
        combat.ReleaseAttackDelegate = ReleaseAttack;
        combat.CancelAttackDelegate = CancelAttack;
    }

    protected IEnumerator Cooldown(float time)
    {
        _CanAttack = false;
        _Anim.SetBool("Attack", true);
        yield return new WaitForSeconds(time);
        _Anim.SetBool("Attack", false);
        _CanAttack = true;
    }

    protected void OnStart()
    {
        if (_Fighting) return;
        _Fighting = true;

        _Anim.SetBool("Combat", true);
        _Crosshair.SetActive(true);
    }

    protected void OnEnd()
    {
        if (!_Fighting) return;
        _Fighting = false;
        _Movement.Aiming = false;
        _Anim.SetBool("Combat", false);
        _Cam.Normal();
        _Crosshair.SetActive(false);
    }
   
    protected abstract void Attack();
    protected abstract bool IsFighting();
    protected abstract void ReleaseAttack();
    protected abstract void CancelAttack();
    protected abstract void CheckSkill(Skill skill);
}
