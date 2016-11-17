using UnityEngine;
using System.Collections;

public enum WeaponType
{
    Bow,
    Sword,
    TwoHandedSword,
    Daggers
}

public class Weapon : MonoBehaviour
{
    public float BaseDamage;
    public WeaponType Type;
}
