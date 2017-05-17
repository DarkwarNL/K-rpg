using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterCombat : MonoBehaviour
{
    private  Animator _Anim;
    private PlayerStats _Stats = new PlayerStats();

    private AudioSource _Source;
    private  float _CurrentAttack = 0;
    private  bool _CanAttack = true;
    private int _AttacksCount =5;
    private Weapon _Weapon;
    private PlayerHealth _Health;
    private WeaponSlot[] _WeaponSlots = new WeaponSlot[2];

    public AudioClip[] Clips = new AudioClip[5];
    public DamageParticle[] Attacks = new DamageParticle[5];
    public ParticleSystem TPParticle;
    public GameObject[] Trails = new GameObject[2];
        
    private void Start()
    {
        _Anim = GetComponent<Animator>();
        _Source = GetComponent<AudioSource>();
        _WeaponSlots = GetComponentsInChildren<WeaponSlot>();
        _Weapon = Resources.Load<Weapon>("Weapons/01");
        SetWeapon(_Weapon);


        _Anim.SetFloat("AttackSpeed", _Stats.AttackSpeed);
    }

    private void Update()
    {
        AnimatorStateInfo stateInfo = _Anim.GetCurrentAnimatorStateInfo(0);

        if (_CanAttack)
        {
            if (stateInfo.IsName("Movement") && Input.GetMouseButtonDown(0))
            {
                _CurrentAttack = 0;
                Attack();                
            }
            else if(Input.GetMouseButton(0))    
            {
                Attack();
            }
            else
            {
                _CurrentAttack = _CurrentAttack = 0;
                _Anim.SetFloat("Attacks", _CurrentAttack);
            }
        }
    }

    public void SetStats(PlayerStats stats)
    {
        _Stats = stats;
        _Anim.SetFloat("AttackSpeed", stats.AttackSpeed);
    }

    public void SetWeapon(Weapon weapon)
    {
        _Weapon = weapon;
        foreach (WeaponSlot slot in _WeaponSlots)
        {
            slot.SetWeapon(weapon.Model);
        }
    }

    private void Attack()
    {
        _CanAttack = false;
        _CurrentAttack++;
        if (_CurrentAttack > _AttacksCount)
            _CurrentAttack = 1;        

        _Anim.SetFloat("Attacks", _CurrentAttack);
    }

    private void Hit()
    {
        _Source.PlayOneShot(Clips[(int)_CurrentAttack -1]);
        Attacks[(int)_CurrentAttack - 1].Particle.Play();
        Vector3 p1 = transform.position;

        RaycastHit[] hit = Physics.SphereCastAll(p1, 1, transform.forward, 2);        

        // Cast a sphere wrapping character controller 10 meters forward
        // to see if it is about to hit anything.
        foreach (RaycastHit h in hit)
        {
            Enemy e = h.transform.GetComponent<Enemy>();
            if (e)
            {
                e.GetHealth().DeltaHealth(Attacks[(int)_CurrentAttack - 1].GetDamage(_Weapon.Damage));
            }
        }
    }

    private void Teleport()
    {
        TPParticle.Play();
    }

    private void GoTP()
    {
        RaycastHit Hit;
        Ray ray = new Ray(transform.position + transform.up, transform.forward);


        if(Physics.Raycast(ray, out Hit, 4 * _Stats.Wisdom))
        {
            transform.position = Hit.point;
        }
        else
            transform.position += (transform.forward * 4) * _Stats.Wisdom;
        End();
    }

    private void StartTrail()
    {
        foreach (GameObject trail in Trails)
        {
            trail.SetActive(true);
        }
    }

    public void End()
    {
        _CanAttack = true;

        foreach (GameObject trail in Trails)
        {
            trail.SetActive(false);
        }
    }
}