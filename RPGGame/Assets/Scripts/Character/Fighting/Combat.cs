using UnityEngine;
using System.Collections;

abstract public class Combat : MonoBehaviour {
    public ParticleSystem SwordTrail;

    protected Animator _Anim;

    protected int _CurrentAttack = 0;
    protected bool _Fighting = false;
    protected bool _CanAttack = true;

    protected float _CombatTime;

    void Awake()
    {
        _Anim = GetComponent<Animator>();
    }

    public bool InCombat()
    {
        return _Fighting;
    }
    
    void FixedUpdate()
    {
        SwitchWeapon(Input.GetAxis("WeaponSwitchX"), Input.GetAxis("WeaponSwitchY"));

        if (Input.GetAxis("Attack") > 0.1f)
        {
            Attack();
            Debug.Log("woppaaa");
        }
       
        if (_Fighting)
        {    
            if ((_CombatTime -= Time.deltaTime )<= 0)
            {
                CombatEnd();
                _Anim.SetTrigger("OnUnsheet");
            }
        }
    }

    void SwitchWeapon(float x, float y)
    {
        if (y != 0)
        {
            if (y < 0)
            {
                Debug.Log("WeaponSwap beneden");
            }
            else
            {
                Debug.Log("WeaponSwap boven");
            }
            _Anim.SetTrigger("OnSheet");

        }
        else if (x != 0)
        {
            if (x < 0)
            {
                Debug.Log("WeaponSwap links");
            }
            else
            {
                Debug.Log("WeaponSwap rechts");
            }
            _Anim.SetTrigger("OnSheet");
        }
    }


    void Attack()
    {
        if (!_Fighting)
        {
            _CurrentAttack = 1;
            _Anim.SetFloat("Attack", _CurrentAttack);

            _Anim.SetBool("Combat", _Fighting = true);
            _CanAttack = false;

            _CombatTime = 5;
        }

        if (_CanAttack)
        {
            _CurrentAttack++;

            _Anim.SetBool("Combat", _Fighting = true);
            _CanAttack = false;

            _CombatTime = 5;
        }     
    }

    public void Hit()
    {
        Debug.Log("GET SHREKKED BITCH");
        _CanAttack = true;
    }

    public void AttackEnd()
    {
        if (_CurrentAttack > 5)
        {
            _CurrentAttack = 1;
        }

        if (_CurrentAttack > _Anim.GetFloat("Attack"))
        {
            _Anim.SetFloat("Attack", _CurrentAttack);
        }
        else
        {
            CombatEnd();
        }
    }

    public void CombatEnd()
    {
        _Anim.SetBool("Combat", false);
        _Fighting = false;
        _CurrentAttack = 0;
        _CanAttack = false;        
    }

    abstract protected void MakeSwitch(int weapon);
}
