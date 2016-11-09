using UnityEngine;
using System.Collections;

public class Enemy_Archer : Enemy {
    private GameObject _Projectile;

    void Start ()
    {
        _Projectile = Resources.Load<GameObject>("Prefabs/Arrow");

        _Name = "Cube Archer";
        _Health.SetMaxHealth(25);
        _AggroRange = 10;

        _Damage = -2;
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
        GameObject arrow = Instantiate(_Projectile, transform.position + transform.forward *2, transform.rotation) as GameObject;        
        arrow.GetComponent<Arrow>().SetDamage(_Damage);
        Destroy(arrow, 3);
    }
}