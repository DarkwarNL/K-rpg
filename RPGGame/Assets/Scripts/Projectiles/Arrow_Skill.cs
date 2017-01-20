using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_Skill : Arrow {
    protected Skill _ArrowSkill;

    public string GetName()
    {
        return _ArrowSkill.SkillName;
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
}
