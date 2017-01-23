using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyHealth))]
abstract public class Enemy : MonoBehaviour {
    private CapsuleCollider _Collider;

    #region PROTECTED_MEMBER_VARIABLES
    //protected vars
    protected EnemyHealth _Health;
    protected NavMeshAgent _Nav;
    protected NavMeshObstacle _Obstacle;
    protected string _Name = "Unkown";

    protected bool _Aggro;

    protected Transform _Target = null;
    protected float _AggroRange = 5;
    protected EnemyAggro _AggroObject;

    protected float _AttackCooldown = 2;
    protected float _AttackRange = 5;
    protected bool _CanAttack = true;
    protected float _Damage = -1;
    protected int _Level = 1;
    protected float _Multiplier = 1.5f;
    protected int DOTPercentage = 40;
    protected int DOTTime = 10;

    protected float _RunSpeed = 3;
    protected float _WalkSpeed = .5f;
    protected Vector3 _StartPos;
    

    #endregion

    #region PRIVATE_MEMBER_VARIABLES

    void Awake()
    {
        _Health = GetComponent<EnemyHealth>();
        _Nav = GetComponent<NavMeshAgent>();
        _Obstacle = GetComponent<NavMeshObstacle>();       

        GameObject obj = new GameObject();
        obj.transform.SetParent(transform);
        _AggroObject = obj.AddComponent<EnemyAggro>();
    }

    void FixedUpdate()
    {
        if (!_Target)
        {
            Idle();
            return;
        }

        _Nav.speed = _RunSpeed;
        EnemyBehaviour();

        if(Vector3.Distance(_Target.position, transform.position) > 20)
        {
            _Target = null;    
            _Nav.SetDestination(_StartPos);
        }
    }
    #endregion

    #region PROTECTED_MEMBER_VARIABLES

    protected IEnumerator Cooldown()
    {
        _CanAttack = false;
        yield return new WaitForSeconds(_AttackCooldown);
        _CanAttack = true;
    }

    protected void SetRotation()
    {
        Vector3 targetDir = (_Target.position + transform.up) - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 2, 0.0F);

        Quaternion newRot = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(newDir), Time.deltaTime * 2);
        newRot.x = transform.rotation.x;
        newRot.z = transform.rotation.z;
        transform.rotation = newRot;
    }
    protected void SetData(float healthValue)
    {
        _StartPos = transform.position;
        _AggroObject.SetData(this, _AggroRange);

        transform.GetComponentInChildren<EnemyName>().SetName(_Name);

        _Damage = _Level * (_Damage * _Multiplier);
        _Health.SetMaxHealth(_Level * (healthValue * _Multiplier));
    }
    protected bool CanAttack()
    {
        if (Vector3.Distance(transform.position, _Target.position) < _AttackRange && _CanAttack)
        {
            _Nav.enabled = false;
            _Obstacle.enabled = true;

            return true;
        }

        _Obstacle.enabled = false;
        _Nav.enabled = true;
        return false;
    }

    protected Vector3 GetRandomPos(Vector3 pos, float radius)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = pos + Random.insideUnitSphere * radius;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }
        return Vector3.zero;
    }

    protected void DamageOverTime(PlayerHealth health)
    {
        if (Random.Range(0, 100) <= DOTPercentage)
        {
            health.SetDOT(_Damage / 5, DOTTime);
        }
    }
    #endregion

    internal void SetTarget(Transform target)
    {
        _Target = target;
    }

    internal EnemyHealth GetHealth()
    {
        return _Health;
    }

    internal void Dead()
    {
        _Nav.Stop();
        _CanAttack = false;
    }

    abstract protected void Attack();
    abstract protected void EnemyBehaviour();
    abstract protected void Idle();
}