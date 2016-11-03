using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Stats), typeof(PlayerHealth), typeof(CharacterMovement))]
public class Player : MonoBehaviour {
    protected Stats _Stats;
    protected PlayerHealth _PlayerHealth;
    protected CharacterMovement _Movement;

    void Awake()
    {
        _Stats = GetComponent<Stats>();
        _PlayerHealth = GetComponent<PlayerHealth>();
        _Movement = GetComponent<CharacterMovement>();
    }
}
