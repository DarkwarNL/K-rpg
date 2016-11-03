using UnityEngine;
using System.Collections;
using System;
[RequireComponent(typeof(Stats))]
public class PlayerHealth : Health {
    public ParticleSystem HealthParticle;
    private float _BaseSpeed;

    private PopupTextCreator _Popup;
    

    void Awake()
    {
        _Popup = PopupTextCreator.Instance;
        _BaseSpeed = HealthParticle.startSpeed;
    }

    private void UpdateHealthParticle()
    {
        float newSpeed = _BaseSpeed * (CurrentHealth / MaxHealth);
        if(newSpeed >= 0)
            HealthParticle.startSpeed = newSpeed;
    }

    protected override void DamageTaken(float amount)
    {
        //_Popup.SpawnDamageText(amount.ToString());
        UpdateHealthParticle();
    }

    protected override void HealTaken(float amount)
    {
       // _Popup.SpawnDamageText(amount.ToString());
        UpdateHealthParticle();
    }

    protected override void Dead()
    {
      //  CurrentHealth = MaxHealth;
    }
}
