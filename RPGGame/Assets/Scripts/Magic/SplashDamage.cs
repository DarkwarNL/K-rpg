using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashDamage : MonoBehaviour
{
    private SphereCollider _Trigger;
    private float _Damage;

    void Start()
    {
        _Trigger = gameObject.AddComponent<SphereCollider>();
        _Trigger.center = new Vector3(0, 0, 0);
        _Trigger.radius = 3;
        _Trigger.isTrigger = true;
    }

    public void SetDamage(float damage)
    {
        _Damage = damage;
    }

    void OnTriggerEnter(Collider other)
    {
        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        if (enemy)
        {
            enemy.DeltaHealth(_Damage);
        }
    }
}
