using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
[RequireComponent(typeof(Stats))]
public class PlayerHealth : Health
{
    private CharacterMovement _Move;
    private Stats _Stats;
    private Vector3 _SavePoint;
    public AudioSource CombatSource;
    public AudioSource NormalSource;

    void Awake()
    {
        _Move = GetComponent<CharacterMovement>();
        _Stats = GetComponent<Stats>();
        SetMaxHealth(50);
        _SavePoint = transform.position;
        InCombat = false;
        StartCoroutine("NormalMusic");
    }

    private IEnumerator NormalMusic()
    {
        float fTimeCounter = 0f;

        while (!(Mathf.Approximately(fTimeCounter, 1f)))
        {
            fTimeCounter = Mathf.Clamp01(fTimeCounter + Time.deltaTime);
            CombatSource.volume = 1f - fTimeCounter;
            NormalSource.volume = fTimeCounter;
            yield return new WaitForSeconds(0.02f);
        }

        while (!InCombat)
        {

            yield return new WaitForSeconds(0.02f);
        }
        if (InCombat)
            StartCoroutine("CombatMusic");
    }

    private IEnumerator CombatMusic()
    {
        float fTimeCounter = 0f;

        while (!(Mathf.Approximately(fTimeCounter, 1f)))
        {
            fTimeCounter = Mathf.Clamp01(fTimeCounter + Time.deltaTime);
            NormalSource.volume = 1f - fTimeCounter;
            CombatSource.volume = fTimeCounter;
            yield return new WaitForSeconds(0.02f);
        }

        while (InCombat)
        {

            yield return new WaitForSeconds(0.02f);
        }
        if (!InCombat)
            StartCoroutine("NormalMusic");
    }

    protected override void UpdateUI()
    {
        _Stats.UpdateUI();
    }

    protected override void DamageTaken(float amount)
    {
        UpdateUI();
       // HitFrom(other);
    }

    protected override void HealTaken(float amount)
    {
        UpdateUI();
    }

    protected override void Dead()
    {
        _Move.Dead();
        Invoke("Respawn", 3);
    }

    protected void Respawn()
    {
        transform.position = _SavePoint;
        _CurrentHealth = _MaxHealth;
        UpdateUI();
    }

    protected void HitFrom(Transform pos)
    {
        Vector3 VectorResult;
        float DotResult = Vector3.Dot(transform.forward, pos.forward);
        if (DotResult > 0)
        {
            VectorResult = transform.forward + pos.forward;
        }
        else
        {
            VectorResult = transform.forward - pos.forward;
        }
        Side hitFrom;

        if (VectorResult.z > 1.8f)
        {
            hitFrom = Side.Front;
        }
        else if (VectorResult.z < -1.8f)
        {
            hitFrom = Side.Back;
        }
        else if (VectorResult.x > 0.8f)
        {
            hitFrom = Side.Right;
        }
        else
        {
            hitFrom = Side.Left;
        }

        _Move.GotHit(hitFrom);
    }

    internal void SetCheckpoint(Vector3 point)
    {
        _SavePoint = point;
    }
}
