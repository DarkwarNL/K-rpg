using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class EnemyHealth : Health
{
    private PopupDamage _Popup;
    private Text HealthValue;
    private Slider HealthSlider;    

    void Awake()
    {
        _Popup = GetComponentInChildren<PopupDamage>();
        HealthSlider = GetComponentInChildren<Slider>();
        HealthValue = HealthSlider.GetComponentInChildren<Text>();
    }

    protected override void DamageTaken(float amount)
    {
        _Popup.SpawnDamageText(amount.ToString("0"));
        GetComponent<Enemy>().SetTarget(FindObjectOfType<Stats>().transform);
    }

    protected override void HealTaken(float amount)
    {
        _Popup.SpawnHealthText(amount.ToString("0"));
    }

    protected override void UpdateUI()
    {
        HealthSlider.maxValue = _MaxHealth;
        HealthSlider.value = _CurrentHealth;
        HealthValue.text = _CurrentHealth + " / " +  _MaxHealth;
        HealthSlider.fillRect.GetComponent<Image>().color = GetColor();
    }

    protected override void Dead()
    {
        Destroy(gameObject, 1);
    }
}
