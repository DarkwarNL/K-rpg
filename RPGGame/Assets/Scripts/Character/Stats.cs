using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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
        _BaseSpeedExp = ExperienceParticle.startSpeed;
        _BaseSpeedHP = HealthParticle.startSpeed;
        UpdateUI();
    }

    internal void DeltaExperience(float delta)
    {
        Experience += delta;
        if (Experience >= MaxExperience)
        {
            Experience -= MaxExperience;
            LevelUp();
        }
    }

    public void UpdateUI()
    {
        float ExpPercentage = (Experience / MaxExperience);
        Debug.Log(ExpPercentage);
        ExperienceParticle.startSpeed = _BaseSpeedExp * ExpPercentage;
        ExperienceBar.fillAmount = ExpPercentage;

        float HPPercentage = _Health.GetPercentage();
        HealthParticle.startSpeed = _BaseSpeedHP * HPPercentage;
        HealthBar.fillAmount = HPPercentage;
    }


    protected void LevelUp()
    {

    }

    protected void Prestige()
    {

    }
}
