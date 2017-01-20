using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_Multi : Arrow_Skill
{
    private ParticleSystem ParticleExplosion;

    void Start()
    {
        _Damage = -20;
        ParticleExplosion = Resources.Load<ParticleSystem>("Particles/MultiArrowExplosion");
    }

    protected override void HitEnemy(Enemy enemy)
    {
        enemy.GetComponent<EnemyHealth>().DeltaHealth(_Damage);
        GameObject splash = Instantiate(ParticleExplosion.gameObject, transform.position, Quaternion.identity);
        Destroy(splash, ParticleExplosion.main.startLifetime.constant);
        splash.AddComponent<SplashDamage>().SetDamage(_Damage);
    }

    protected override void HitOther(Collider other)
    {
       
    }

    protected override void HitPlayer(PlayerHealth player)
    {
        
    }
}
