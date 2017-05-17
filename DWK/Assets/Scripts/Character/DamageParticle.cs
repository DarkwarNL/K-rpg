using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageParticle : MonoBehaviour {
    public Skill Skill;
    internal ParticleSystem Particle;

    private void Awake()
    {
        Particle = GetComponent<ParticleSystem>();
    }

    internal float GetDamage(float baseDamage)
    {
        return Skill.GetDamage(baseDamage);
    }
}
