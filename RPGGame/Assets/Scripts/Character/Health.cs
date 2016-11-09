using UnityEngine;
using System.Collections;

abstract public class Health : MonoBehaviour
{
    protected float _CurrentHealth = 8;
    protected float _MaxHealth = 8;

    protected float _HealthRegeneration = 1;
    internal float RegenerationCD = 0;
    internal float RegenerationTime = 5;
    internal bool InCombat = false;
    
    public void DeltaHealth(float delta)
    {
        _CurrentHealth += delta;
        InCombat = true;

        if(delta > 0)
        {
            DamageTaken(delta);
        }
        else
        {
            HealTaken(delta);
        }
        UpdateUI();

        if (_CurrentHealth <= 0)
        {
            Dead();
        }
    }

    public void SetMaxHealth(float value)
    {
        _MaxHealth = value;
        _CurrentHealth = _MaxHealth;
    }

    protected Color GetColor()
    {
        Color newColor = new Color();
        if (_CurrentHealth > _MaxHealth / 2)//white to gray
        {
            newColor = Color.Lerp(Color.green, Color.yellow, _CurrentHealth / _MaxHealth * 2 - 1);
        }
        else//gray to red
        {
            newColor = Color.Lerp(Color.yellow, Color.red, _CurrentHealth / _MaxHealth * 2);
        }
        return newColor;
    }

    float Regen()
    {
        float regen = _HealthRegeneration;

        if (InCombat)
            regen = regen / 2;

        RegenerationCD = RegenerationTime;
        return regen;
    }

    void FixedUpdate()
    {
        if (RegenerationCD <= 0 && _CurrentHealth < _MaxHealth)
            DeltaHealth(Regen());
    }

    abstract protected void UpdateUI();
    abstract protected void HealTaken(float amount);
    abstract protected void DamageTaken(float amount);
    abstract protected void Dead();
}
