using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(EnemyHealth), typeof(NavMeshAgent), typeof(CapsuleCollider))]
abstract public class Enemy : MonoBehaviour {

    //private vars

    private CapsuleCollider _Collider;

    #region PROTECTED_MEMBER_VARIABLES
    //protected vars
    protected EnemyHealth _Health;
    protected NavMeshAgent _Nav;
    protected string _Name = "Unkown";

    protected bool _Aggro;

    protected Transform _Target = null;
    protected float _AggroRange = 5;
    protected GameObject AggroObject;

    protected float _AttackCooldown = 2;
    protected float _AttackRange = 5;
    protected bool _CanAttack = true;

    protected float _RunSpeed = 3;
    protected float _WalkSpeed = .5f;
    protected Vector3 _StartPos;

    #endregion

    protected void SetData()
    {
        transform.GetComponentInChildren<EnemyName>().SetName(_Name);
        _Collider.radius = _AggroRange;
        _StartPos = transform.position;
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

    #region PRIVATE_MEMBER_VARIABLES

    void Awake()
    {
        _Health = GetComponent<EnemyHealth>();
        _Nav = GetComponent<NavMeshAgent>();
        _Collider = GetComponent<CapsuleCollider>();
        _Collider.isTrigger = true;
    }

    void FixedUpdate()
    {
        if (!_Target)
        {
            Idle();
            return;
        }

        if (_Target)
        {
            _Nav.SetDestination(_Target.transform.position);
            _Nav.speed = _RunSpeed;
        }
        if (_Nav.remainingDistance < _AttackRange && _CanAttack && _Target)
        {
            _Nav.Stop();
            Attack();
            StartCoroutine(Cooldown());
        }
        else if(Vector3.Distance(transform.position, _StartPos) > 20)
        {
            _Nav.Resume();
            _Target = null;            
            _Aggro = false;
            _Nav.SetDestination(_StartPos);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Stats>())
        {            
            GameObject instance = Instantiate(Resources.Load("Prefabs/EnemyAggroComp", typeof(GameObject)),transform.position, Quaternion.identity) as GameObject;
            instance.GetComponent<AggroComponent>().SetTarget(_Target);
            Destroy(instance, 5);
            _Target = other.transform;
            _Aggro = true;
        }
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

    abstract protected void Attack();
    abstract protected void Idle();
}