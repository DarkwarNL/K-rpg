using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

[Serializable]
public class Skill
{  
    internal string SkillName;
    internal Sprite Sprite;

    internal Arrow Arrow;

    protected float _CastCooldown = 5;
    private bool CanCastSkill = true;
    private Image _CooldownImage;

    public Skill(float cooldown, string name, Sprite sprite, Arrow arrow)
    {
        _CastCooldown = cooldown;        
        SkillName = name;
        Sprite = sprite;
        Arrow = arrow;
    }

    internal IEnumerator CastCooldown()
    {
        CanCastSkill = false;
        yield return new WaitForSeconds(_CastCooldown);
        CanCastSkill = true;
    }

    private IEnumerator SetImageCooldown()
    {
        float endTime = Time.time + _CastCooldown;
        while (Time.time < endTime)
        {
            _CooldownImage.fillAmount = (endTime - Time.time) / _CastCooldown;
            yield return null;
        }
    }

    public bool CanCast()
    {
        return CanCastSkill;
    }
}

public class OffensiveSkill : MonoBehaviour
{
    protected Skill _Skill;
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