using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_Elemental : Arrow
{
    public ArrowType ArrowType;
    public SplashDamage ExplosionParticle;

    protected override void HitEnemy(Enemy enemy)
    {
        // Switch on the Priority enum.
        switch (ArrowType)
        {
            case ArrowType.Exploding:
                ExplosionParticle.SetDamage(_Damage * 1.5f);
                break;
            case ArrowType.Elemental:
                ExplosionParticle.SetDamage(_Damage * 1.2f);
                break;
            case ArrowType.Normal:
                ExplosionParticle.SetDamage(_Damage);
                break;
        }
        Destroy(Instantiate(ExplosionParticle.gameObject, transform.position, Quaternion.identity), 3);

        enemy.GetHealth().DeltaHealth(_Damage);
    }

    protected override void HitOther(Collider other)
    {
    }

    protected override void HitPlayer(PlayerHealth player)
    {
    }
}
