using UnityEngine;
using System.Collections;

public class Archer : Combat
{
    void Start()
    {
        _Weapons = Resources.LoadAll<GameObject>("Prefabs/Weapons/Archer");
    }

    protected override void Attack()
    {
        if (!_Fighting)
        {
            Unsheet();
            _Anim.SetTrigger("Aim");
            _Fighting = true;            
        }
        _CombatTime = 10;
    }
}