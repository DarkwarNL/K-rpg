using UnityEngine;
using System.Collections;

public enum ArrowType
{
    Normal,
    Elemental,
    Exploding,

}

[RequireComponent(typeof(BoxCollider))]
abstract public class Arrow : MonoBehaviour {
    protected float _Speed = 25f;
    protected float _Damage = 2f;
    protected GameObject _Owner;
    protected bool _Holding = false;
    protected GameObject _Target;

    void Awake()
    {
        BoxCollider col = GetComponent<BoxCollider>();
        col.isTrigger = true;
    }
    
	void FixedUpdate ()
    {
        if (_Holding) return;
        if (_Target) transform.LookAt(_Target.transform);

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

    internal void SetData(float speed, float damage, GameObject owner, GameObject target)
    {
        _Speed = speed;
        _Damage = damage;
        _Owner = owner;
        _Target = target;
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
            //transform.SetParent(player.transform);
            if (GetComponent<TrailRenderer>()) Destroy(GetComponent<TrailRenderer>());
            Destroy(this);
        }
        else if (enemy)
        {
            HitEnemy(enemy);
            if(!_Owner.GetComponent<Enemy>())
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
