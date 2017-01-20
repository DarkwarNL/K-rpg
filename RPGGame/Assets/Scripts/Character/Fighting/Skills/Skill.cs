using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

[Serializable]
public class Skill
{  
    public string SkillName;
    public Sprite Sprite;
    
    public Arrow Arrow;

    protected float _CastCooldown = 5;
    private bool CanCastSkill = true;
    private Image _CooldownImage;

    public Skill()
    {

    }

    public Skill(float cooldown, string name, Sprite sprite, Arrow arrow)
    {
        _CastCooldown = cooldown;        
        SkillName = name;
        Sprite = sprite;
        Arrow = arrow;
    }

    public void SetCooldownImage(Image img)
    {
        _CooldownImage = img;
    }

    internal IEnumerator SetImageCooldown()
    {
        float endTime = Time.time + _CastCooldown;
        while (Time.time < endTime)
        {
            _CooldownImage.fillAmount = (endTime - Time.time) / _CastCooldown;
            yield return null;
        }
    }

    internal IEnumerator CastCooldown()
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

    public string GetName()
    {
        return _Skill.SkillName;
    }
}