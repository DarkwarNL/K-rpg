using UnityEngine;
using System.Collections;
using System;

public class EnemyHealth : Health
{
 //   private DamageTextCreator _Popup;


    void Awake()
    {
  //      _Popup = PopupTextCreator.Instance;
    }

    protected override void DamageTaken(float amount)
    {
        Debug.Log("hit enemy" + amount);
    }

    protected override void HealTaken(float amount)
    {

    }

    protected override void Dead()
    {
        Debug.Log("rip");
    }
}
