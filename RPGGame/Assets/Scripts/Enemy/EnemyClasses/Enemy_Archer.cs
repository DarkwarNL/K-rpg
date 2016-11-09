using UnityEngine;
using System.Collections;

public class Enemy_Archer : Enemy {
    void Start ()
    {
        _Name = "Cube Archer";
        _Health.SetMaxHealth(25);
        _AggroRange = 10;

        _AttackCooldown = 2.5f;
        _AttackRange = 7.5f;

        SetData();
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
        Debug.Log("pew");
    }
}