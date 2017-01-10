using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum ElementType
{
    Fire,
    Electric,
    Ice
}

public class Stats : MonoBehaviour {
    public Image HealthBar;
    public Image ExperienceBar;
    public ParticleSystem HealthParticle;
    public ParticleSystem ExperienceParticle;
    private float _BaseSpeedExp;
    private float _BaseSpeedHP;
    /// <summary>
    /// Stats
    /// </summary>
    private PlayerHealth _Health;
    private Combat _Combat;

    internal float HealthRegeneration = 1;

    internal float Experience = 0;
    internal float MaxExperience = 80;
    internal int ExperienceRanking = 1;

    private static Stats _Stats;

    public static Stats Instance
    {
        get
        {
            if(!_Stats)
               _Stats = FindObjectOfType<Stats>();
            return _Stats;
        }
    }

    internal bool GetInCombat()
    {
        return _Combat.InCombat();
    }

    void Awake()
    {
        _Health = GetComponent<PlayerHealth>();
        _Combat = GetComponent<Combat>();
        _BaseSpeedExp = ExperienceParticle.main.startSpeed.constant;
        _BaseSpeedHP = HealthParticle.main.startSpeed.constant;
        UpdateUI();
    }

    internal void DeltaExperience(float delta)
    {
        Experience += delta /10;

        if (Experience >= MaxExperience)
        {
            Experience -= MaxExperience;
            LevelUp();
        }

        UpdateUI();
    }

    public void UpdateUI()
    {
        float ExpPercentage = (Experience / MaxExperience);
        var mainExp = ExperienceParticle.main;
        mainExp.startSpeed = _BaseSpeedExp * ExpPercentage;
        ExperienceBar.fillAmount = ExpPercentage;

        if (!_Health) return;
        float HPPercentage = _Health.GetPercentage();
        var mainHP = HealthParticle.main;
        mainHP.startSpeed = _BaseSpeedHP * HPPercentage;
        HealthBar.fillAmount = HPPercentage;
    }

    protected void LevelUp()
    {

    }

    protected void Prestige()
    {

    }
}
