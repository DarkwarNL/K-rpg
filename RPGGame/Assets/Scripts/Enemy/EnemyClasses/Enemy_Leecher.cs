using UnityEngine;
using System.Collections;

public class Enemy_Leecher : Enemy
{
    void Start ()
    {
        _Name = "Leecher";
        _AggroRange = 10;

        _Level = Random.Range(1, 4);
        _Damage = -2;
        _AttackCooldown = 2.5f;
        _AttackRange = 4f;
        _RunSpeed = 2f;

        SetData(15);
    }

    protected override void Idle()
    {
        if(_Nav.remainingDistance < 2)
        {            
            _Nav.SetDestination(GetRandomPos());
            
            _Nav.speed = _WalkSpeed;
        }
    }
    protected override void Attack()
    {
        Vector3 pos = _Target.position - transform.position;
        var distance = pos.magnitude;
        var angle = Vector3.Angle(pos, transform.forward);
        var thing = transform.forward * _AttackRange;
        var thing1 = Quaternion.Euler(0, 45, 0) * thing;
        var thing2 = Quaternion.Euler(0, -45, 0) * thing;
        
        if (distance <= _AttackRange && angle <= 45)
        {
            _Target.GetComponent<PlayerHealth>().DeltaHealth(_Damage);
            DamageOverTime(_Target.GetComponent<PlayerHealth>());
        }
    }
}