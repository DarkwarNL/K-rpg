using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_MissileArrows : OffensiveSkill
{
    private ParticleSystem _Part;
    public List<ParticleCollisionEvent> collisionEvents;

    void Awake()
    {
        _Part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = _Part.GetCollisionEvents(other, collisionEvents);

        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        int i = 0;

        while (i < numCollisionEvents)
        {
            if (enemy)
            {
                enemy.DeltaHealth(CalculateDamage() / 10);
            }
            i++;
        }
    }
}