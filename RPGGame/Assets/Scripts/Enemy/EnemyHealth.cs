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
        _Popup.CreateFloatingDamageText(Statics.GetNumber(amount), "Damage");
        GetComponent<Enemy>().SetTarget(FindObjectOfType<Stats>().transform);
    }

    protected override void HealTaken(float amount)
    {
        _Popup.CreateFloatingDamageText(Statics.GetNumber(amount), "Heal");
    }

    protected override void UpdateUI()
    {
        HealthSlider.maxValue = _MaxHealth;
        HealthSlider.value = _CurrentHealth;
        HealthValue.text = Statics.GetNumber(_CurrentHealth) + " / " + Statics.GetNumber(_MaxHealth);
        HealthSlider.fillRect.GetComponent<Image>().color = Color.red;
    }

    protected override void Dead()
    {
        transform.GetComponent<Enemy>().Dead();
        Destroy(gameObject, 1);
    }
}
