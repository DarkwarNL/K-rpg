using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
[RequireComponent(typeof(Stats))]
public class PlayerHealth : Health {
    private PopupTextCreator _Popup;   

    void Awake()
    {
        _Popup = PopupTextCreator.Instance;
        SetMaxHealth(100);
    }

    protected override void UpdateUI()
    {
        Stats.Instance.UpdateUI();
    }

    protected override void DamageTaken(float amount)
    {
        _Popup.SpawnDamageText(Statics.GetNumber(amount));
        UpdateUI();
    }

    protected override void HealTaken(float amount)
    {
        _Popup.SpawnDamageText(Statics.GetNumber(amount));
        UpdateUI();
    }

    protected override void Dead()
    {
      //  CurrentHealth = MaxHealth;
    }
}
