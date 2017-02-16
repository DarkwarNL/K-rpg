using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;

[Serializable]
public abstract class Enemy : MonoBehaviour{	
	private string _EnemyName;
	private int _EnemyID;
	private float _MovementSpeed;
	private float _Health;
	private float _KillReward;
	private GameObject _Model;
    	
    private NavMeshAgent _Nav;
    private Vector3 _Target;

    void Awake()
    {
        FindTarget();
    }

    protected void FindTarget()
    {
        _Nav = gameObject.GetComponent<NavMeshAgent>();
        _Target = GameObject.FindGameObjectWithTag("Target").transform.position;
        _Nav.SetDestination(_Target);
    }

    protected IEnumerator Stunned(float time)
    {
        _Nav.Stop();
        yield return new WaitForSeconds(time);
        _Nav.Resume();
    }

    public void Stun(float time)
    {
        StartCoroutine(Stunned(time));
    }

    public void DeltaHealth(float delta)
    {
        _Health += delta;
        if(_Health <= 0)
        {
            Dead();
        }
    }

    protected abstract void Dead();
}
