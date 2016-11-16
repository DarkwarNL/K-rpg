using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
[RequireComponent(typeof(Stats))]
public class PlayerHealth : Health {
    private float _BaseSpeed;

    private PopupTextCreator _Popup;   

    void Awake()
    {
        _Popup = PopupTextCreator.Instance;
    }

    protected override void UpdateUI()
    {
        Stats.Instance.UpdateUI();
    }

    protected override void DamageTaken(float amount)
    {
        _Popup.SpawnDamageText(amount.ToString());
        UpdateUI();
    }

    protected override void HealTaken(float amount)
    {
        _Popup.SpawnDamageText(amount.ToString());
        UpdateUI();
    }

    protected override void Dead()
    {
      //  CurrentHealth = MaxHealth;
    }
}
