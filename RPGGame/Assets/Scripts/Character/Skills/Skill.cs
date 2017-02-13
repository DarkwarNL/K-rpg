using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

[CreateAssetMenu]
public class Skill : ScriptableObject
{
    [SerializeField]
    public string SkillName;
    public int SkillNumber { get; private set; }

    public float PowerMultiplier = 2;
    public Sprite Sprite;
    public float Cooldown = 5;
    public bool CanCastSkill = true;

    public Skill()
    {

    }

    public void SetCooldownImage(int num)
    {
        SkillNumber = num;
    }

    internal IEnumerator SetImageCooldown()
    {
        Image cooldownImage = ActionBar.Instance.GetCooldownImage(SkillNumber);
        float endTime = Time.time + Cooldown;
        while (Time.time < endTime)
        {
            cooldownImage.fillAmount = (endTime - Time.time) / Cooldown;
            yield return null;
        }
    }

    internal IEnumerator CastCooldown()
    {
        CanCastSkill = false;        
        yield return new WaitForSeconds(Cooldown);
        CanCastSkill = true;
    }

    public bool CanCast()
    {
        return CanCastSkill;
    }
}

public class OffensiveSkill : MonoBehaviour
{
    public Skill Skill;
    protected float _Damage = 5;
    protected float _DamageCooldown = 5;
     // 150% of base weapon damage
    protected bool _CanDamage = true;

    protected float CalculateDamage()
    {
        return _Damage * Skill.PowerMultiplier;
    }

    protected IEnumerator DamageCooldown()
    {
        _CanDamage = false;
        yield return new WaitForSeconds(_DamageCooldown);
        _CanDamage = true;
    }

    public string GetName()
    {
        return Skill.SkillName;
    }
}