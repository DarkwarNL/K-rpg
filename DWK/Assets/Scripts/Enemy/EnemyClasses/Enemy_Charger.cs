using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Enemy_Charger : Enemy
{
    float _ChargeCooldown = 10;
    bool _CanCharge = true;
    void Start ()
    {
        
        _AggroRange = 10;

        _Level = Random.Range(2, 4);
        _Damage = -2;
        _AttackCooldown = 1.5f;
        _AttackRange = 2.5f;
        _RunSpeed = 3f;
        SetData(15);
    }

    protected override void EnemyBehaviour()
    {
        AnimatorStateInfo stateInfo = _Anim.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName(AnimAttackString))
        {
            _Nav.isStopped = true;
        }
        else
            _Nav.isStopped = false;

        if (CanAttack() && stateInfo.IsName(AnimMovementString))
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
        else if (_CanCharge && _DistanceFromTarget >= 5)
        {
            StartCoroutine(Charge());
        }

        if (!_Nav.isActiveAndEnabled) return;        

        if (_Nav.remainingDistance <= _Nav.stoppingDistance || Vector3.Distance(_Target.position, _Nav.destination) > _AttackRange * 1.5f)
            _Nav.SetDestination(GetRandomPos(_Target.position, _AttackRange * 1.5f));
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

        SetRotation();
        if (distance <= _AttackRange * add && angle <= 60 )
        {
            if (_Target.GetComponent<PlayerHealth>().DeltaHealth(_Damage))
                _Target = null;
        }
        _Anim.SetBool(AnimAttackString, false);
        _AttackTimer = 0;
    }

    private IEnumerator Charge()
    {
        _Disabled = true;

        _Nav.destination = _Target.position + transform.forward;
        while (Vector3.Distance(transform.position, _Nav.destination) > _AttackRange -3)
        {
            if (!_Target)
                break;
            _Nav.speed = 9;
            _Anim.SetFloat("Speed", _Nav.speed);
            SetRotation();

            AnimatorStateInfo stateInfo = _Anim.GetCurrentAnimatorStateInfo(0);
            if (!stateInfo.IsName("Movement"))
                _Nav.speed = 0;

            if (_Nav.remainingDistance < 1f)
            {
                SetRotation();
                _CanCharge = false;
                _Nav.speed = _RunSpeed;
                _Disabled = false;

                yield return new WaitForSeconds(_ChargeCooldown);
                _CanCharge = true;

                yield break;
            }
            yield return null;
        }
    }
}