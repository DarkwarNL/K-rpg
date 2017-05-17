using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Enemy_GuardianHKU : Enemy
{
    float _ChargeCooldown = 30;
    bool _CanCharge = true;
    void Start()
    {
        _AggroRange = 10;

        _Level = Random.Range(20, 30);
        _Damage = -1;
        _AttackCooldown = 2.5f;
        _AttackRange = 4f;
        _RunSpeed = 6f;
        _WalkSpeed = 3;
        _ResetDistance = 100;
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
        else if (_CanCharge)
        {
            StartCoroutine(Charge());
        }

        if (!_Nav.isActiveAndEnabled) return;

        if (_Nav.remainingDistance <= _Nav.stoppingDistance || Vector3.Distance(_Target.position, _Nav.destination) > _AttackRange * 1.5f)
            _Nav.SetDestination(GetRandomPos(_Target.position, _AttackRange * 1.5f));
    }

    protected override void Idle()
    {
        if (_Nav.remainingDistance < 2)
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
        if (distance <= _AttackRange * add && angle <= 60)
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

        while (Vector3.Distance(transform.position, _Nav.destination) > _AttackRange - 3)
        {
            if (!_Target)
                break;

            _Nav.destination = _Target.position;
            _Nav.speed = 10;
            SetRotation();

            AnimatorStateInfo stateInfo = _Anim.GetCurrentAnimatorStateInfo(0);
            if (!stateInfo.IsName("Movement"))
                _Nav.speed = 0;

            if (Vector3.Distance(transform.position, _Target.position) < _AttackRange + .5f)
            {
                SetRotation();
                _Anim.SetTrigger("JumpAttack");
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