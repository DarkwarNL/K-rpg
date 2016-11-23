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
        combat.AimDelegate = Aim;
        combat.AttackDelegate = Attack;
        combat.IsFightingDelegate = IsFighting;
        combat.ReleaseAimDelegate = ReleaseAim;
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

    protected abstract void Aim();
    protected abstract void Attack();
    protected abstract bool IsFighting();
    protected abstract void ReleaseAim();
    protected abstract void ReleaseAttack();
}
