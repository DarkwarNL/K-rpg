using UnityEngine;
using System.Collections;
[RequireComponent(typeof(CapsuleCollider))]
public class EnemyAggro : MonoBehaviour {

    private Enemy _Enemy;

    public void SetData(Enemy enemy, float aggroRange)
    {
        transform.name = "AggroObject";
        _Enemy = enemy;

        CapsuleCollider col = GetComponent<CapsuleCollider>();
        col.isTrigger = true;
        col.radius = aggroRange;        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Stats>())
        {
            GameObject aggro = Instantiate(Resources.Load("Prefabs/EnemyAggroComp", typeof(GameObject)), transform.position, Quaternion.identity) as GameObject;
            aggro.GetComponent<AggroComponent>().SetTarget(other.transform);
            Destroy(aggro, 5);
            _Enemy.SetTarget(other.transform);
        }
    }
}
