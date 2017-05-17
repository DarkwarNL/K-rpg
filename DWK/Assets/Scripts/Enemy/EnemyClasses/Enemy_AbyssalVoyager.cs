using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Enemy_AbyssalVoyager : Enemy
{
    void Start ()
    {
        _Name = "Abyssal Voyager";
        _AggroRange = 10;

        _Level = Random.Range(1, 4);
        _Damage = -2;
        _AttackCooldown = 2.5f;
        _AttackRange = 1.5f;
        _RunSpeed = 2f;

        SetData(15);
    }

    protected override void EnemyBehaviour()
    {
        if (CanAttack())
        {
            SetRotation();

            Vector3 pos = _Target.position - transform.position;
            var angle = Vector3.Angle(pos, transform.forward);

            if (angle <= 20)
            {
                _AttackTimer = 0;
                _Anim.SetFloat("AttackType", Random.Range(0, 5));
                _Anim.SetBool(AnimAttackString, true);
            }
        }

        if (_Nav.remainingDistance <= _Nav.stoppingDistance || Vector3.Distance(_Target.position, _Nav.destination) > _AttackRange * 1.5f)
            _Nav.SetDestination(GetRandomPos(_Target.position, _AttackRange * 1.5f));
    }

    protected override void Idle()
    {
        if(_Nav.remainingDistance < 2)
        {            
            _Nav.SetDestination(GetRandomPos(_StartPos, 10));
            
            _Nav.speed = _WalkSpeed;
            _Anim.SetBool("Walking", true);
        }
    }

    internal override void Attack(float add)
    {
        Vector3 pos = _Target.position - transform.position;
        var distance = pos.magnitude;
        var angle = Vector3.Angle(pos, transform.forward);

        SetRotation();
        if (distance <= _AttackRange * add && angle <= 60)
        {
            if (_Target.GetComponent<PlayerHealth>().DeltaHealth(_Damage))
                _Target = null;
        }
        _Anim.SetBool(AnimAttackString, false);
        _AttackTimer = 0;
    }
}