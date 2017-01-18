using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_RainOfArrows : Arrow_Skill
{
    protected string _Name = "RainOfArrows";

    private GameObject _Particle;

    void Start()
    {
        _Speed = 15;
        _Particle = Resources.Load<GameObject>("Prefabs/ArcherParticleAttacks/Particle_RainOfArrows");
        
    }

    void Update()
    {
        if (!GetComponent<Rigidbody>() && !_Holding)
        {
            gameObject.AddComponent<Rigidbody>().mass = 1000f;
        }
    }

    protected override void HitEnemy(Enemy enemy)
    {
            
    }

    protected override void HitOther(Collider other)
    {
        if (other.name != "Terrain") return;
        _Holding = true;
        Destroy(Instantiate(_Particle, transform.position + (transform.up * 10), Quaternion.identity), 5);
        transform.SetParent(other.transform, true);
        Destroy(GetComponent<Rigidbody>());
    }

    protected override void HitPlayer(PlayerHealth player)
    {

    }

    public override string GetName()
    {
        return _Name;
    }
}
