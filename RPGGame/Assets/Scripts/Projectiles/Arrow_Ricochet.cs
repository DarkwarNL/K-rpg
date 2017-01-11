using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_Ricochet : Arrow {

    private Arrow StandardArrow;
    private int _TargetCount = 3;

    void Start()
    {
        _Damage = -20;
        StandardArrow = Resources.Load<Arrow>("Prefabs/Arrows/RicochetSubArrow");
    }

    protected override void HitEnemy(Enemy enemy)
    {
        
        Enemy[] enemies =  FindObjectsOfType<Enemy>();
        List<Enemy> targets = new List<Enemy>();

        foreach(Enemy e in enemies)
        {            
            if(Vector3.Distance(transform.position, e.transform.position)< 10)
            {
                targets.Add(e);
                if (targets.Count == _TargetCount) break;
            }
        }

        if(targets.Count > 0)
        {
            for(int i = 0; i < targets.Count; i++)
            {
                Arrow arrow = Instantiate(StandardArrow.gameObject, transform.position, Quaternion.identity).GetComponent<Arrow>();
                arrow.SetData(10, _Damage * .8f, enemy.gameObject, targets[i].gameObject);
                Destroy(arrow.gameObject, 10);
            }
        }
        enemy.GetComponent<EnemyHealth>().DeltaHealth(_Damage);
    }

    protected override void HitOther(Collider other)
    {
    }

    protected override void HitPlayer(PlayerHealth player)
    {
    }
}