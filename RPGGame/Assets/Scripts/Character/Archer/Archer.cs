using UnityEngine;
using System.Collections;
using System;

public class Archer : Combat
{
    public Arrow _SelectedArrow;
    private ArrowSlot _ArrowSlot;

    void Start()
    {
        _Weapons = Resources.LoadAll<GameObject>("Prefabs/Weapons/Archer");
        _ArrowSlot = GetComponentInChildren<ArrowSlot>();
    }

    protected override void Attack()
    {
        if (!_Fighting)
        {
            UnSheet();                      
        }
        _Anim.SetBool("Aim", true);
        _ArrowSlot.SpawnArrow(_SelectedArrow);
    }

    protected override void Release()
    {
        _Anim.SetBool("Aim" , false);
        _ArrowSlot.Release();
    }
}