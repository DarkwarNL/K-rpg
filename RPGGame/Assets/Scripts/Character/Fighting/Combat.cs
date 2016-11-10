using UnityEngine;
using System.Collections;

abstract public class Combat : MonoBehaviour {
    protected Weapon _SelectedWeapon;
    protected GameObject[] _Weapons;
    protected WeaponSlot _WeaponSlot;
    protected Animator _Anim;

    protected int _CurrentAttack = 0;
    protected bool _Fighting = false;
    protected bool _CanAttack = true;

    protected float _CombatTime = 10;

    void Awake()
    {
        _Anim = GetComponent<Animator>();
        _WeaponSlot = GetComponentInChildren<WeaponSlot>();
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
                MakeSwitch(2);
            }
            else
            {
                MakeSwitch(0);
            }
        }
        else if (x != 0)
        {
            if (x < 0)
            {
                MakeSwitch(3);
            }
            else
            {
                MakeSwitch(1);
            }            
        }
    }

    protected void MakeSwitch(int weapon)
    {
        if (_Weapons.Length > weapon)
        {
            Weapon wep = _Weapons[weapon].GetComponent<Weapon>();
            if (_WeaponSlot.CanSwitch(wep))
            {
                _SelectedWeapon = wep;
                _Anim.SetTrigger("OnSheet");
                _Fighting = true;
                _CombatTime = 10;
            }
        }
    }

    protected void UnSheet()
    {
        if (!_SelectedWeapon)
        {
            _SelectedWeapon = _Weapons[0].GetComponent<Weapon>();
        }
        _WeaponSlot.UnSheet();
        _Anim.SetTrigger("OnSheet");
    }

    public void Hit()
    {
        _CanAttack = true;
    }

    public void CombatEnd()
    {
        _Fighting = false;
        _CurrentAttack = 0;
        _CanAttack = false;
        _WeaponSlot.Sheet(); 
    }

    protected abstract void Attack();
}
