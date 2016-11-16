using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
abstract public class Arrow : MonoBehaviour {
    protected float _Speed = 25f;
    protected float _Damage = 2f;
    protected GameObject _Owner;
    protected bool _Holding = false;

    void Awake()
    {
        BoxCollider col = GetComponent<BoxCollider>();
        col.isTrigger = true;        
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
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _Owner || !_Owner || _Holding) return;

        PlayerHealth player = other.GetComponent<PlayerHealth>();
        Enemy enemy = other.GetComponent<Enemy>();
        
        if (player)
        {
            HitPlayer(player);
            Destroy(gameObject);
        }
        else if (enemy)
        {
            HitEnemy(enemy);
            Destroy(gameObject);
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
