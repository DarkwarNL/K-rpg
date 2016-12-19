using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Combat))]
abstract public class CombatStyle : MonoBehaviour {
    protected CameraFollow _Cam;
    protected CharacterMovement _Movement;
    protected Animator _Anim;
    protected Weapon _Weapon;
    protected GameObject _Crosshair;
    protected bool _CanAttack = true;

    protected bool _Fighting;

    void Awake()
    {
        _Cam = CameraFollow.Instance;
        _Movement = GetComponent<CharacterMovement>();

        _Crosshair = GameObject.FindGameObjectWithTag("Crosshair");
    }

    internal void SetDelegates(Combat combat, Animator anim, Weapon weapon)
    {
        _Anim = anim;
        _Weapon = weapon;
        combat.AttackDelegate = Attack;
        combat.IsFightingDelegate = IsFighting;
        combat.ReleaseAttackDelegate = ReleaseAttack;
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
}
