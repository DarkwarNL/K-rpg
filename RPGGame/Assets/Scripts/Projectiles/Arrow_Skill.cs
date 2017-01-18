using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Arrow_Skill : Arrow
{
    void Start()
    {

    }

    protected override void HitEnemy(Enemy enemy)
    {

    }

    protected override void HitOther(Collider other)
    {

    }

    protected override void HitPlayer(PlayerHealth player)
    {

    }

    public abstract string GetName();
}
