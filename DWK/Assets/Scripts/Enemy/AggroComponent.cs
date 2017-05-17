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
        transform.localPosition = new Vector3();
    }

    public void SetTarget(Transform target)
    {
        _Target = target;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Stats>()) _Target = other.transform;

        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy && _Target)
        {
            enemy.SetTarget(_Target);
        }
    }
}