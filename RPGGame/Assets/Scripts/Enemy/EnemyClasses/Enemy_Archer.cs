using UnityEngine;
using System.Collections;
using System;

public class Enemy_Archer : Enemy
{
    private GameObject _Projectile;

    void Start()
    {
        _Projectile = Resources.Load<GameObject>("Prefabs/Arrows/Arrow");

        _Name = "Cube Archer";
        _AggroRange = 10;

        _Damage = -2;
        _AttackCooldown = 2.5f;
        _AttackRange = 7.5f;

        SetData(25);
    }

    protected override void EnemyBehaviour()
    {
        if (CanAttack())
        {
            SetRotation();

            Vector3 pos = _Target.position - transform.position;
            var angle = Vector3.Angle(pos, transform.forward);
            
            if (angle <= 7.5f || pos.magnitude <= 5 && angle <= 25f)
            {
               Transform newTrans = _Target;
               newTrans.position += transform.up;
               transform.LookAt(newTrans);
                
                Attack();
            }
        }
        if (!_Nav.isActiveAndEnabled) return;

        if (_Nav.remainingDistance <= _Nav.stoppingDistance)
            _Nav.SetDestination(GetRandomPos(_Target.position, _AttackRange * 2));
    }

    protected override void Idle()
    {
        if (_Nav.remainingDistance < 2)
        {
            _Nav.SetDestination(GetRandomPos(_StartPos, 10));

            _Nav.speed = _WalkSpeed;
        }
    }

    protected override void Attack()
    {
        GameObject arrow = Instantiate(_Projectile, transform.position + transform.forward * 2, transform.rotation) as GameObject;
        arrow.GetComponent<Arrow>().SetData(_Damage, gameObject);

        StartCoroutine(Cooldown());
    }
}