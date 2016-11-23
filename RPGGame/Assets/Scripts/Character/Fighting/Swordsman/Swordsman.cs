using UnityEngine;
using System.Collections;
using System;

public class Swordsman : CombatStyle
{
    private bool _Targetted;
    private int _CurrentAttack;

    protected override void Aim()
    {
        _Movement.Aiming = true;
        _Cam.Aiming();
        _Targetted = true;
        _Anim.SetBool("Aim", true);
    }

    protected override void Attack()
    {
        if (!_CanAttack) return;
        for (int i = 0; i < _Anim.layerCount; i++)
        {
            AnimatorStateInfo stateInfo = _Anim.GetCurrentAnimatorStateInfo(i);
            if (stateInfo.IsName("Sword_Movement"))
            {
                if (_CurrentAttack > 5) _CurrentAttack = 0;
                _CurrentAttack++;
                _Anim.SetFloat("CurrentAttack", _CurrentAttack);
                StartCoroutine(Cooldown(.5f));
            }
        }
    }

    public void Strike()
    {
        Enemy[] targets = FindObjectsOfType<Enemy>();

        foreach (Enemy enemy in targets)
        {
            Vector3 pos = enemy.transform.position - transform.position;
            var distance = pos.magnitude;
            var angle = Vector3.Angle(pos, transform.forward);

            if (distance <= 4 && angle <= 75)
            {
                enemy.GetComponent<Health>().DeltaHealth(-15);
                GetComponent<Stats>().DeltaExperience(15);
            }
        }
    }

    protected override bool IsFighting()
    {
        return _Targetted;
    }

    protected override void ReleaseAim()
    {
        _CurrentAttack = 0;
        _Anim.SetFloat("CurrentAttack", _CurrentAttack);
        _Targetted = false;
    }

    protected override void ReleaseAttack()
    {
        _CurrentAttack = 0;
        _Anim.SetFloat("CurrentAttack", _CurrentAttack);
    }
}
