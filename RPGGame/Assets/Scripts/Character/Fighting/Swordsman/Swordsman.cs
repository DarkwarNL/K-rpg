using UnityEngine;
using System.Collections;
using System;

public class Swordsman : CombatStyle
{
    private int _CurrentAttack;
    public int AnimLayer;

    void Awake()
    {
    }

    protected override void CheckSkill(int skill)
    {
    }

    protected override void Attack()
    {
        OnStart();
        if (!_CanAttack) return;

        AnimatorStateInfo stateInfo = _Anim.GetCurrentAnimatorStateInfo(AnimLayer);
        if (stateInfo.IsName("Movement"))
        {
            if (_CurrentAttack > 5) _CurrentAttack = 0;
            _CurrentAttack++;
            _Anim.SetFloat("CurrentAttack", _CurrentAttack);
            StartCoroutine(Cooldown(.3f));
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
        return _Fighting;
    }
    
    protected override void ReleaseAttack()
    {
        OnEnd();
        _CurrentAttack = 0;
        _Anim.SetFloat("CurrentAttack", _CurrentAttack);       
    }

    protected override void CancelAttack()
    {
        
    }
}
