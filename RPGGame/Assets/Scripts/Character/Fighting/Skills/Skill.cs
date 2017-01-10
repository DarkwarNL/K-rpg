using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    protected float _CastCooldown = 5;
    public bool CanCastSkill = true;
    public Sprite Sprite;

    public IEnumerator CastCooldown()
    {
        CanCastSkill = false;
        yield return new WaitForSeconds(_CastCooldown);
        CanCastSkill = true;
    }

    public bool CanCast()
    {
        return CanCastSkill;
    }
}

public class OffensiveSkill : Skill
{
    protected float _Damage = -10;
    protected float _DamageCooldown = 5;
    protected float _DamageMultiplier = 2; // 150% of base weapon damage
    protected bool _CanDamage = true;

    protected float CalculateDamage()
    {
        return _Damage * _DamageMultiplier;
    }

    protected IEnumerator DamageCooldown()
    {
        _CanDamage = false;
        yield return new WaitForSeconds(_DamageCooldown);
        _CanDamage = true;
    }

}