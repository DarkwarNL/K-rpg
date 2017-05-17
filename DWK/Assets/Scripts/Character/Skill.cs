using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Skill : ScriptableObject {
    public float DamageMultiplier;

    public float GetDamage(float baseDamage)
    {
        return baseDamage * DamageMultiplier;
    }
}
