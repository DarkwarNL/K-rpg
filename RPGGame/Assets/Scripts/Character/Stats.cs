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
    [SerializeField]
    private Image _HealthBar;
    [SerializeField]
    private Image _ExperienceBar;
    [SerializeField]
    private ParticleSystem _HealthParticle;
    [SerializeField]
    private ParticleSystem _ExperienceParticle;

    private float _BaseSpeedExp;
    private float _BaseSpeedHP;
    /// <summary>
    /// Stats
    /// </summary>

    public Player Player;
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
                
        _BaseSpeedExp = _ExperienceParticle.main.startSpeed.constant;
        _BaseSpeedHP = _HealthParticle.main.startSpeed.constant;
        UpdateUI();


        SaveLoad.Load();
        Player = SaveLoad.GetPlayer(Player.PlayerName);
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
        var mainExp = _ExperienceParticle.main;
        mainExp.startSpeed = _BaseSpeedExp * ExpPercentage;
        _ExperienceBar.fillAmount = ExpPercentage;

        if (!_Health) return;
        float HPPercentage = _Health.GetPercentage();
        var mainHP = _HealthParticle.main;
        mainHP.startSpeed = _BaseSpeedHP * HPPercentage;
        _HealthBar.fillAmount = HPPercentage;
    }

    protected void LevelUp()
    {

    }

    protected void Prestige()
    {

    }
}
