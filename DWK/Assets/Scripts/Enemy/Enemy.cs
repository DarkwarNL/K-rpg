using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyHealth), typeof(NavMeshAgent))]
abstract public class Enemy : MonoBehaviour {
    private CapsuleCollider _Collider;

    #region PROTECTED_MEMBER_VARIABLES
    //protected vars
    protected EnemyHealth _Health;
    protected NavMeshAgent _Nav;
    protected NavMeshObstacle _Obstacle;
    protected AudioSource _Source;
    protected Animator _Anim;
    [SerializeField]
    protected string _Name = "Unkown";

    protected bool _Aggro;

    protected Transform _Target = null;
    protected float _AggroRange = 5;
    protected EnemyAggro _AggroObject;

    protected float _AttackCooldown = 2;
    protected float _AttackTimer;
    protected float _AttackRange = 5;
    protected bool _CanAttack = true;
    protected float _Damage = -1;
    protected int _Level = 1;
    protected float _Multiplier = 1.5f;
    protected int DOTPercentage = 10;
    protected int DOTTime = 3;

    protected float _RunSpeed = 3;
    protected float _WalkSpeed = 1f;
    protected float _CurrentSpeed;
    protected Vector3 _StartPos;
    protected bool _Disabled = false;
    protected float _DistanceFromTarget = 10;
    protected float _ResetDistance = 50;

    protected const string AnimMovementString = "Movement";
    protected const string AnimAttackString = "Attack";

    #endregion

    #region PRIVATE_MEMBER_FUNTIONS

    void Awake()
    {
        _Source = GetComponent<AudioSource>();
        _Health = GetComponent<EnemyHealth>();
        _Nav = GetComponent<NavMeshAgent>();
        _Anim = GetComponent<Animator>();
        GameObject obj = new GameObject();
        obj.transform.SetParent(transform);
        _AggroObject = obj.AddComponent<EnemyAggro>();
        _StartPos = transform.position;

        _Anim.SetFloat("DeadType", Random.Range(0, 2));
    }

    void FixedUpdate()
    {
        MovementController();
        
        if(_AttackTimer >= _AttackCooldown)
        {
            _CanAttack = true;
        }
        else
        {
            _CanAttack = false;
            _AttackTimer += Time.deltaTime;
        }
    }
    #endregion

    #region PROTECTED_MEMBER_VARIABLES

    protected void MovementController()
    {
        float newSpeed = 0;

        if (_Disabled)
            return;
            
        if (!_Target)
        {
            Idle();
            newSpeed = _Nav.speed = _WalkSpeed;
            _Nav.angularSpeed = 200;

            _Anim.SetFloat("Speed", _WalkSpeed);
            _Health.InCombat = false;
            return;
        }
        _Target.GetComponent<Health>().CombatTime = 5;
        _Health.InCombat = true;

        EnemyBehaviour();
        AnimatorStateInfo stateInfo = _Anim.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName(AnimMovementString))
        {
            newSpeed = _RunSpeed;
            if (_DistanceFromTarget > 6)
            {
                _Nav.angularSpeed = 200;
            }
            else
            {
                SetRotation();
            }

            if (_DistanceFromTarget < _AttackRange)
                newSpeed = 0;
        }
        else if (stateInfo.IsName(AnimAttackString))
        {
            SetRotation();
            newSpeed = 0;
        }

        _CurrentSpeed = Mathf.Lerp(_CurrentSpeed, newSpeed, Time.deltaTime * 5);
        if (_CurrentSpeed <= 0.01f)
            _CurrentSpeed = 0;

        _Nav.speed = _CurrentSpeed;
        _Anim.SetFloat("Speed", _CurrentSpeed);
        
        _DistanceFromTarget = Vector3.Distance(_Target.position, transform.position);
        if (Vector3.Distance(_StartPos, transform.position) > _ResetDistance)
        {
            _Target.GetComponent<Health>().InCombat = false;
            _Target = null;
            _Nav.SetDestination(_StartPos);
        }
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
        _Nav.stoppingDistance = _AttackRange -.5f;
        _StartPos = transform.position;
        _AggroObject.SetData(this, _AggroRange);
       // transform.GetComponentInChildren<EnemyName>().SetName(_Name);

        _Damage = _Level * (_Damage * _Multiplier);
        _Health.SetMaxHealth(_Level * (healthValue * _Multiplier));
    }

    protected bool CanAttack()
    {
        if (_DistanceFromTarget <= _AttackRange && _CanAttack)
        {
            return true;
        }

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

    protected float GetDamage()
    {
        return Random.Range(_Damage - 7.5f, _Damage);
    }
    #endregion

    internal void SetTarget(Transform target)
    {
        if (_Target)
            return;
        _Target = target;
    }

    internal EnemyHealth GetHealth()
    {
        return _Health;
    }

    internal void Dead()
    {
        _Anim.SetBool("Dead", true);
        _Target.GetComponent<Stats>().DeltaExperience(_Level * 5);
        _Target.GetComponent<Health>().InCombat = false;
        Destroy(gameObject, 10);
        _Nav.speed = 0;
        this.enabled = false;
    }

    internal void PlaySound(string name)
    {
        _Source.PlayOneShot(Resources.Load<AudioClip>("Sounds/Enemy/"+name));
    }

    abstract internal void Attack(float add);
    abstract protected void EnemyBehaviour();
    abstract protected void Idle();
}