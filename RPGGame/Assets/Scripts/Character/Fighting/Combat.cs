using UnityEngine;
using System.Collections;

abstract public class Combat : MonoBehaviour {
    protected Weapon _SelectedWeapon = null;
    protected GameObject[] _Weapons;
    protected WeaponSlot _WeaponSlot;
    protected Animator _Anim;

    protected int _CurrentAttack = 0;
    protected bool _Fighting = false;
    protected bool _Aiming = true;
    protected bool _CanShoot = true;
    protected float _AttackSpeed = 2;
    protected float _CombatTime = 0;

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

        if (Input.GetAxis("Aiming") > 0.1f)
        {
            Aim();
        }
        else
        {
            Release();
        }

        if (Input.GetAxis("Attack") > 0.1f && _Aiming && _CanShoot)
        {            
            Shoot();
        }
               
        if (_Fighting && !_Aiming)
        {    
            if ((_CombatTime += Time.deltaTime )>= 10)
            {
                CombatEnd();                
                _Anim.SetTrigger("Sheath");
            }
        }
    }

    protected IEnumerator Cooldown()
    {
        _CanShoot = false;
        _Anim.SetBool("Shoot", true);
        yield return new WaitForSeconds(1);
        _Anim.SetBool("Shoot", false);
        _CanShoot = true;
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
                _Anim.SetTrigger("UnSheath");
                _Fighting = true;
                _CombatTime = 10;
            }
        }
    }

    protected void UnSheath()
    {
        if (!_SelectedWeapon)
        {
            _SelectedWeapon = _Weapons[0].GetComponent<Weapon>();
            _WeaponSlot.CanSwitch(_SelectedWeapon);
        }
        else
        {
            _WeaponSlot.UnSheath();
        }
        _Anim.SetTrigger("UnSheath");
        _Fighting = true;
    }
    public void CombatEnd()
    {
        _CombatTime = 0;
        _Fighting = false;
        _CurrentAttack = 0;
        _WeaponSlot.Sheath(); 
    }

    protected abstract void Aim();
    protected abstract void Shoot();
    protected abstract void Release();
}
