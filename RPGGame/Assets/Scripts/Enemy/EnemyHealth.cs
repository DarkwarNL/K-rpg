using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class EnemyHealth : Health
{
    private PopupTextCreator _Popup;
    private Text HealthValue;
    private Slider HealthSlider;

    void Awake()
    {
        _Popup = PopupTextCreator.Instance;
        HealthSlider = GetComponentInChildren<Slider>();
        HealthValue = HealthSlider.GetComponentInChildren<Text>();

        DeltaHealth(0);    
    }

    protected override void DamageTaken(float amount)
    {

    }

    protected override void HealTaken(float amount)
    {

    }

    protected override void UpdateUI()
    {
        HealthSlider.value = _CurrentHealth;
        HealthValue.text = _MaxHealth + " / " + _CurrentHealth;
        HealthSlider.fillRect.GetComponent<Image>().color = GetColor();
    }

    protected override void Dead()
    {
        Debug.Log("rip");
    }
}
