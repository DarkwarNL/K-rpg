using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Skill : MonoBehaviour
{  
    public string ArrowLocation;
    public Sprite Sprite;

    public Arrow Arrow;   

    protected float _CastCooldown = 5;
    private bool CanCastSkill = true;
    private Image _CooldownImage;

    void Start()
    {
        Arrow = Resources.Load<Arrow>("Prefabs/Arrows/" + ArrowLocation);

        transform.GetChild(0).GetComponent<Image>().sprite = Sprite;
        _CooldownImage = transform.GetChild(1).GetComponent<Image>();
    }

    public void CastSkill()
    {
        StartCoroutine(CastCooldown());
    }

    internal IEnumerator CastCooldown()
    {
        CanCastSkill = false;
        StartCoroutine(SetImageCooldown());
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