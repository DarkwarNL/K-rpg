﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
[RequireComponent(typeof(Stats))]
public class PlayerHealth : Health {

    void Awake()
    {
        SetMaxHealth(50);
    }

    protected override void UpdateUI()
    {
        Stats.Instance.UpdateUI();
    }

    protected override void DamageTaken(float amount)
    {
        UpdateUI();
    }

    protected override void HealTaken(float amount)
    {
        UpdateUI();
    }

    protected override void Dead()
    {
      //  CurrentHealth = MaxHealth;
    }
}
