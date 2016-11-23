using UnityEngine;
using System.Collections;
using System;

public class Arrow_Basic : Arrow
{
    protected override void HitEnemy(Enemy enemy)
    {
        enemy.GetHealth().DeltaHealth(_Damage);        
    }

    protected override void HitOther(Collider other)
    {
        
    }

    protected override void HitPlayer(PlayerHealth player)
    {
        player.DeltaHealth(_Damage);
    }
}
