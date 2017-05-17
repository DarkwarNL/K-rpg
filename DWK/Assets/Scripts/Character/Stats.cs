using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum ElementType
{
    Fire,
    Electric,
    Ice
}

[System.Serializable]
public class PlayerStats
{
    public float MovementSpeed;
    public float AttackSpeed = 0.8f;
    public float Wisdom = 1;

    internal void LevelUP()
    {
        MovementSpeed += 0.1f;
        AttackSpeed += 0.1f;
        Wisdom += 0.1f;
    }
}

[RequireComponent(typeof(CharacterCombat), typeof(CharacterMovement))]
public class Stats : MonoBehaviour {    
    private PlayerHealth _Health;
    private CharacterCombat _Combat;
    private CharacterMovement _Move;

    [SerializeField]
    private PlayerStats _PlayerStats = new PlayerStats();

    public float _Experience = 0;
    private float _XPForLevel = 80;
  
    void Awake()
    {
        _Health = GetComponent<PlayerHealth>();
        _Combat = GetComponent<CharacterCombat>();
        _Move = GetComponent<CharacterMovement>();

        UpdateUI();
    }

    private void Update()
    {
        _Combat.SetStats(_PlayerStats);
        _Move.SetStats(_PlayerStats);
    }

    internal PlayerStats GetStats()
    {
        return _PlayerStats;
    }

    internal void DeltaExperience(float delta)
    {
        _Experience += delta;

        if (_Experience >= _XPForLevel)
        {
            _Experience -= _XPForLevel;
            LevelUp();
        }

        UpdateUI();
    }

    public void UpdateUI()
    {
    }

    protected void LevelUp()
    {
        _XPForLevel *= 2.1f;
        _PlayerStats.LevelUP();
    }
}
