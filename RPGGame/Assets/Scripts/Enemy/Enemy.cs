using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EnemyHealth), typeof(NavMeshAgent), typeof(CapsuleCollider))]
public class Enemy : MonoBehaviour {
    //protected vars
    protected EnemyHealth _Health;
    protected NavMeshAgent _Nav;

    protected bool _Aggro;
    protected Transform _Target;
    protected float _AggroRange;

    //public vars
    public GameObject AggroObject;

    void Awake()
    {
        _Health = GetComponent<EnemyHealth>();
        _Nav = GetComponent<NavMeshAgent>();
        CapsuleCollider col = GetComponent<CapsuleCollider>();
        col.radius = _AggroRange;
        
        col.isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(AggroObject);
            _Target = other.transform;
            _Aggro = true;            
        }
    }
}
