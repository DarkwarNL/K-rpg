using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
abstract public class Arrow : MonoBehaviour {
    protected float _Speed = 25f;
    protected float _Damage = 2f;
    protected GameObject _Owner;
    protected bool _Holding = false;
    protected TrailRenderer _Trail;

    void Awake()
    {
        BoxCollider col = GetComponent<BoxCollider>();
        col.isTrigger = true;
        _Trail = GetComponent<TrailRenderer>();
    }
    
	void FixedUpdate ()
    {
        if (_Holding) return;
        Vector3 forward = transform.forward * Time.deltaTime * _Speed;
        transform.position += forward;
	}

    internal void Release()
    {
        _Holding = false;
        _Trail.enabled = true;
    }

    internal void SetDamage(float damage)
    {
        _Damage = damage;
    }

    internal void SetData(float speed, float damage, GameObject owner)
    {
        _Speed = speed;
        _Damage = damage;
        _Owner = owner;
    }

    internal void SetData(float damage, GameObject owner)
    {
        _Damage = damage;
        _Owner = owner;
    }

    internal void SetData(float damage, GameObject owner, bool holding)
    {
        _Damage = damage;
        _Owner = owner;
        _Holding = holding;

        _Trail.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _Owner || !_Owner || _Holding) return;

        PlayerHealth player = other.GetComponent<PlayerHealth>();
        Enemy enemy = other.GetComponent<Enemy>();
        
        if (player)
        {
            HitPlayer(player);
            transform.SetParent(player.transform);
            if (GetComponent<TrailRenderer>()) Destroy(GetComponent<TrailRenderer>());
            Destroy(this);
        }
        else if (enemy)
        {
            HitEnemy(enemy);
            _Owner.GetComponent<Stats>().DeltaExperience(_Damage);
            transform.SetParent(enemy.transform);
            if (GetComponent<TrailRenderer>()) Destroy(GetComponent<TrailRenderer>());
            Destroy(this);
        }
        else
        {
            HitOther(other);
        }
    }
    protected abstract void HitPlayer(PlayerHealth player);
    protected abstract void HitEnemy(Enemy enemy);
    protected abstract void HitOther(Collider other);
}
