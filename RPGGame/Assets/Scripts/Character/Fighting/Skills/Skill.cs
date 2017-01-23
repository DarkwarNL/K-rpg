using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

[Serializable]
public class Skill
{  
    public string SkillName { get; private set; }
    public int SkillNumber { get; private set; }

    private string Sprite;    
    private string Arrow;

    protected float _CastCooldown = 5;
    private bool CanCastSkill = true;

    public Skill()
    {

    }

    public Skill(float cooldown, string name, string sprite, string arrow)
    {
        _CastCooldown = cooldown;        
        SkillName = name;
        Sprite = sprite;
        Arrow = arrow;
    }

    public Sprite GetSprite()
    {
        return Resources.Load<Sprite>(Sprite);
    }

    public Arrow GetArrow()
    {
        return Resources.Load<Arrow>(Arrow);
    }

    public void SetCooldownImage(int num)
    {
        SkillNumber = num;
    }

    internal IEnumerator SetImageCooldown()
    {
        Image cooldownImage = ActionBar.Instance.GetCooldownImage(SkillNumber);
        float endTime = Time.time + _CastCooldown;
        while (Time.time < endTime)
        {
            cooldownImage.fillAmount = (endTime - Time.time) / _CastCooldown;
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