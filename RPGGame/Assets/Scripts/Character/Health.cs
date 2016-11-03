using UnityEngine;
using System.Collections;

abstract public class Health : MonoBehaviour
{
    public float CurrentHealth = 8;
    public float MaxHealth = 8;

    public float HealthRegeneration = 1;
    internal float RegenerationCD = 0;
    internal float RegenerationTime = 5;
    internal bool InCombat = false;
    
    public void DeltaHealth(float delta)
    {
        CurrentHealth += delta;
        InCombat = true;

        if(delta > 0)
        {
            DamageTaken(delta);
        }
        else
        {
            HealTaken(delta);
        }
        
        if(CurrentHealth <= 0)
        {
            Dead();
        }
    }

    float Regen()
    {
        float regen = HealthRegeneration;

        if (InCombat)
            regen = regen / 2;

        RegenerationCD = RegenerationTime;
        return regen;
    }

    void FixedUpdate()
    {
        if (RegenerationCD <= 0 && CurrentHealth < MaxHealth)
            DeltaHealth(Regen());
    }

    abstract protected void HealTaken(float amount);
    abstract protected void DamageTaken(float amount);
    abstract protected void Dead();
}
