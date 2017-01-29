using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Bow,
    Sword,
    TwoHandedSword,
    Daggers
}

[CreateAssetMenu]
public class Weapon : Item {
    public WeaponType WeaponType;

    public float GetDamage()
    {
        return Random.Range(MinumumPower, MaximumPower);
    }
}
