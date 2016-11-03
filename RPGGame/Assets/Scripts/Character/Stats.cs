using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {
    public ParticleSystem ExperienceParticle;
    private float _BaseSpeed;

    /// <summary>
    /// Stats
    /// </summary>
    private PlayerHealth _Health;
    private Combat _Combat;

    internal float HealthRegeneration = 1;

    internal float Experience = 0;
    internal float MaxExperience = 80;
    internal int ExperienceRanking = 1;

    internal float MovementSpeed = 3.5f;

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
        _BaseSpeed = ExperienceParticle.startSpeed;
    }

    void Update()
    {
        _Health.DeltaHealth(-Time.deltaTime/2);
        DeltaExperience(Time.deltaTime *10);
    }

    internal void DeltaExperience(float delta)
    {
        Experience += delta;
        if (Experience >= MaxExperience)
        {
            Experience -= MaxExperience;
            LevelUp();
        }

        ExperienceParticle.startSpeed = _BaseSpeed * (Experience / MaxExperience);
    }

    internal void LevelUp()
    {

    }

    internal void Prestige()
    {

    }
}
