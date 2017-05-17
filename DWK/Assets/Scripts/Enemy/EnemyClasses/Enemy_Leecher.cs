using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Enemy_Leecher : Enemy
{
    void Start ()
    {
        _AggroRange = 10;

        _Level = Random.Range(1, 4);
        _Damage = -2;
        _AttackCooldown = 2.5f;
        _AttackRange = 1.5f;
        _RunSpeed = 1f;

        SetData(15);
    }

    protected override void EnemyBehaviour()
    {   
        AnimatorStateInfo stateInfo = _Anim.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName(AnimAttackString))
        {
            _Nav.enabled = false;
        }
        else
            _Nav.enabled = true;

        if (CanAttack() && stateInfo.IsName(AnimMovementString))
        {
            SetRotation();

            Vector3 pos = _Target.position - transform.position;
            var angle = Vector3.Angle(pos, transform.forward);
            
            if (angle <= 25)
            {
                _Anim.SetFloat("AttackType", Random.Range(0, 2));
            }
        }
        if (!_Nav.isActiveAndEnabled) return;

        if (_Nav.remainingDistance <= _Nav.stoppingDistance)
            _Nav.SetDestination(GetRandomPos(_Target.position, _AttackRange *1.5f));
    }

    protected override void Idle()
    {
        if(_Nav.remainingDistance < 2)
        {            
            _Nav.SetDestination(GetRandomPos(_StartPos, 10));
            
            _Nav.speed = _WalkSpeed;
        }
    }

    internal override void Attack(float add)
    {      
        Vector3 pos = _Target.position - transform.position;
        var distance = pos.magnitude;
        var angle = Vector3.Angle(pos, transform.forward); 
        
        if (distance <= _AttackRange && angle <= 45)
        {
            _Target.GetComponent<PlayerHealth>().DeltaHealth(_Damage);
            DamageOverTime(_Target.GetComponent<PlayerHealth>());
        }
    }
}