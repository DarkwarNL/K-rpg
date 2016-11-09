using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
abstract public class Arrow : MonoBehaviour {
    protected float _Speed = 15f;
    protected float _Damage = 2f;
    internal GameObject _Parent;

    void Awake()
    {
        BoxCollider col = GetComponent<BoxCollider>();
        col.isTrigger = true;        
    }
    
	void FixedUpdate ()
    {
        Vector3 forward = transform.forward * Time.deltaTime * _Speed;
        transform.position += forward;
	}

    internal void SetDamage(float damage)
    {
        _Damage = damage;
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerHealth player = other.GetComponent<PlayerHealth>();
        Enemy enemy = other.GetComponent<Enemy>();
        
        if (player)
        {
            HitPlayer(player);
        }
        else if (enemy)
        {
            HitEnemy(enemy);
        }
        else
        {
            HitOther(other);
        }
        Destroy(gameObject);
    }
    protected abstract void HitPlayer(PlayerHealth player);
    protected abstract void HitEnemy(Enemy enemy);
    protected abstract void HitOther(Collider other);
}
