using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_RainOfArrows : OffensiveSkill
{
    private BoxCollider _Trigger;
    private List<EnemyHealth> enemies = new List<EnemyHealth>();

    void Awake()
    {
        _DamageMultiplier = 2;
        _DamageCooldown = 2;

        _Trigger = gameObject.AddComponent<BoxCollider>();
        _Trigger.center = new Vector3(0, -5, 0);
        _Trigger.size = new Vector3(10, 15, 10);
        _Trigger.isTrigger = true;
    }

    internal void SetPosition(Vector3 pos)
    {
        pos.y += 10;
        transform.position = pos;
    }

    void OnTriggerEnter(Collider other)
    {
        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        if (enemy)
        {
            enemies.Add(enemy);
        }
    }

    void OnTriggerExit(Collider other)
    {
        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        if (enemy)
        {
            enemies.Remove(enemy);
        }
    }
    
    void FixedUpdate()
    {
        if (_CanDamage && enemies.Count > 0)
        {
            for(int i = 0; i < enemies.Count; i++)
            {
                if (!enemies[i])
                {
                    enemies.RemoveAt(i);
                    continue;
                }

                if (enemies[i].DeltaHealth(CalculateDamage()))
                {
                    enemies.RemoveAt(i);
                }
            }
            StartCoroutine(DamageCooldown());
        }
    } 
}