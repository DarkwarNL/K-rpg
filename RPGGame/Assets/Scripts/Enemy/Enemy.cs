using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(EnemyHealth), typeof(NavMeshAgent))]
abstract public class Enemy : MonoBehaviour {
    private CapsuleCollider _Collider;

    #region PROTECTED_MEMBER_VARIABLES
    //protected vars
    protected EnemyHealth _Health;
    protected NavMeshAgent _Nav;
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

    protected void SetData(float healthValue)
    {
        _StartPos = transform.position;
        _AggroObject.SetData(this, _AttackRange);
    
        transform.GetComponentInChildren<EnemyName>().SetName(_Name);
        transform.GetComponentInChildren<EnemyLevel>().SetLevel( "Lv " + _Level.ToString());       

        _Damage = _Level * (_Damage * _Multiplier);
        _Health.SetMaxHealth(_Level * (healthValue * _Multiplier));
    }

    protected Vector3 GetRandomPos()
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = _StartPos + Random.insideUnitSphere * 4;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }    
        return Vector3.zero;
    }

    protected void DamageOverTime(PlayerHealth health)
    {
        if(Random.Range(0, 100) <= DOTPercentage)
        {
            health.SetDOT(_Damage / 5, DOTTime);
        }
    }

    #region PRIVATE_MEMBER_VARIABLES

    void Awake()
    {
        _Health = GetComponent<EnemyHealth>();
        _Nav = GetComponent<NavMeshAgent>();

        GameObject obj = new GameObject();
        obj.transform.SetParent(transform);
        _AggroObject = obj.AddComponent<EnemyAggro>();
    }

    void FixedUpdate()
    {
        if (!_Target)
        {
            Idle();
            _Nav.stoppingDistance = 1;
            return;
        }
        if (_Target)
        {
            _Nav.SetDestination(_Target.transform.position);
            _Nav.speed = _RunSpeed;
            _Nav.stoppingDistance = _AttackRange;
        }

        if (_Nav.remainingDistance < _AttackRange && _Target)
        {
            SetRotation();
            if (_CanAttack)
            {
                Attack();
                StartCoroutine(Cooldown());
            }
        }
        else if(Vector3.Distance(transform.position, _StartPos) > 20)
        {
            _Target = null;    
            _Nav.SetDestination(_StartPos);
        }
    }

    void SetRotation()
    {
        Vector3 targetDir = _Target.position - transform.position;
        
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 5,0);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(newDir), 8 * Time.deltaTime);
    }
        
    IEnumerator Cooldown()
    {
        _CanAttack = false;
        yield return new WaitForSeconds(_AttackCooldown);
        _CanAttack = true;
        _Nav.Resume();
    }
    #endregion

    public void SetTarget(Transform target)
    {
        _Target = target;
    }

    internal EnemyHealth GetHealth()
    {
        return _Health;
    }

    abstract protected void Attack();
    abstract protected void Idle();
}