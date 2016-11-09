using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
public class AggroComponent : MonoBehaviour {
    private Transform _Target = null;

    void Awake()
    {
        SphereCollider col = GetComponent<SphereCollider>();
        col.radius = 10;
        col.isTrigger = true;
    }

    public void SetTarget(Transform target)
    {
        _Target = target;
    }

    void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy)
        {
            enemy.SetTarget(_Target);
        }
    }
}