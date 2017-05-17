using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class EnemyHealth : Health
{
    private FloatingDamageManager _Popup;
    private Text HealthValue;
    private Slider HealthSlider;    

    void Awake()
    {
        _Popup = GetComponentInChildren<FloatingDamageManager>();
        HealthSlider = GetComponentInChildren<Slider>();
        HealthSlider.maxValue = _MaxHealth;
        HealthSlider.fillRect.GetComponent<Image>().color = Color.red;

        //HealthValue = HealthSlider.GetComponentInChildren<Text>();
    }

    protected override void DamageTaken(float amount)
    {
        _Popup.ShowDMG(Statics.GetNumber(amount));
        UpdateUI();
    }

    protected override void HealTaken(float amount)
    {
       // _Popup.CreateFloatingDamageText(Statics.GetNumber(amount), "Heal");
    }

    protected override void UpdateUI()
    {
        HealthSlider.maxValue = _MaxHealth;
        HealthSlider.fillRect.GetComponent<Image>().color = Color.red;
        HealthSlider.value = _CurrentHealth;
        //HealthValue.text = Statics.GetNumber(_CurrentHealth) + " / " + Statics.GetNumber(_MaxHealth);
        
    }

    protected override void Dead()
    {
        GetComponentInChildren<Canvas>().gameObject.SetActive(false);
        transform.GetComponent<Enemy>().Dead();
    }
    
}
