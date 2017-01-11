using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_Multi : Arrow
{
    private Arrow StandardArrow;
    public int _ArrowCount = 20;

    void Start()
    {
        _Damage = -20;
        StandardArrow = Resources.Load<Arrow>("Prefabs/Arrows/MultiSubArrow");
    }

    protected override void HitEnemy(Enemy enemy)
    {
        enemy.GetComponent<EnemyHealth>().DeltaHealth(_Damage);

        for(int i = 0; i < _ArrowCount; i++)
        {
            Arrow arrow = Instantiate(StandardArrow, transform.position, Quaternion.identity);
            arrow.SetData(10, _Damage * .8f, enemy.gameObject);

            arrow.transform.rotation = Quaternion.Euler(0, 15 * i, 0);
            Destroy(arrow.gameObject, 1.5f);

        }
    }

    protected override void HitOther(Collider other)
    {
       
    }

    protected override void HitPlayer(PlayerHealth player)
    {
        
    }


}
