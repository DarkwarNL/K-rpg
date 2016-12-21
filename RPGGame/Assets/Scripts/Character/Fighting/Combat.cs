using UnityEngine;
using System.Collections;

public class Combat : MonoBehaviour {
    protected Weapon _SelectedWeapon = null;
    protected GameObject[] _Weapons;
    protected WeaponSlot _WeaponSlotLeft;
    protected WeaponSlot _WeaponSlotRight;
    protected Animator _Anim;
    protected WeaponTabUI _WeaponTab;

    protected float _CombatTime = 0;

    internal delegate void Attack();
    internal delegate void ReleaseAttack();
    internal delegate bool IsFighting();

    internal Attack AttackDelegate;
    internal ReleaseAttack ReleaseAttackDelegate;
    internal IsFighting IsFightingDelegate;

    void Awake()
    {
        _Weapons = Resources.LoadAll<GameObject>("Prefabs/Weapons");
        _Anim = GetComponent<Animator>();
        _WeaponSlotLeft = GetComponentInChildren<WeaponSlot>();
        _WeaponSlotRight = GetComponentInChildren<ArrowSlot>().transform.parent.GetComponentInChildren<WeaponSlot>();
        GetComponent<Swordsman>().SetDelegates(this, _Anim, _SelectedWeapon);
        _WeaponTab = WeaponTabUI.Instance;
    }

    public bool InCombat()
    {
        return IsFightingDelegate();
    }
    
    void FixedUpdate()
    {
        SwitchWeapon(Input.GetAxis("WeaponSwitchX"), Input.GetAxis("WeaponSwitchY"));

        if (Input.GetMouseButton(0))
        {
            WeaponCheck(_SelectedWeapon);
            AttackDelegate();
        }
        else //if(Input.GetAxis("Attack") <= 0)
        {
            for (int i = 0; i < _Anim.layerCount; i++)
            {
                AnimatorStateInfo stateInfo = _Anim.GetCurrentAnimatorStateInfo(i);
                if (stateInfo.IsName("Movement"))
                {
                    ReleaseAttackDelegate();
                    if(_SelectedWeapon)
                        if(_SelectedWeapon.IsSheathed)
                            Sheath();
                }
            }
        }
               
        if (!IsFightingDelegate())
        {    
            if ((_CombatTime += Time.deltaTime )>= 10)
            {
                Sheath();               
            }
        }
    }

    void SwitchWeapon(float x, float y)
    {
        if (y != 0)
        {
            if (y < 0)
            {
                MakeSwitch(0); // bow
            }
            else
            {
                MakeSwitch(3); // Sword
            }
        }
        else if (x != 0)
        {
            if (x < 0)
            {
                MakeSwitch(2); // 2Handed
            }
            else
            {
                MakeSwitch(1); // daggers
            }            
        }
    }

    protected void MakeSwitch(int number)
    {
        if (_Weapons.Length > number)
        {
            Weapon weapon = _Weapons[number].GetComponent<Weapon>();
            if (weapon != _SelectedWeapon)
            {
                WeaponCheck(weapon);
            }
        }
    }

    protected void WeaponCheck(Weapon weapon)
    {
        if (_SelectedWeapon != weapon && !IsFightingDelegate())
        {
            UnSheath(weapon);
        }
        else if (!_SelectedWeapon)
        {
            UnSheath(_Weapons[0].GetComponent<Weapon>());
        }
        else if (!_SelectedWeapon.IsSheathed && !IsFightingDelegate())
        {
            UnSheath(_SelectedWeapon);
        }
    }

    protected void UnSheath(Weapon weapon)
    {
        if(_SelectedWeapon)
            _SelectedWeapon.IsSheathed = false;
        _WeaponSlotLeft.Sheath();
        _WeaponSlotRight.Sheath();

        _SelectedWeapon = weapon;
        _SelectedWeapon.IsSheathed = true;        
        _Anim.SetTrigger("UnSheath");
        _WeaponTab.SelectedWeapon(weapon.Type);

        int layer = 0;
        switch (_SelectedWeapon.Type)
        {
            case WeaponType.Bow:
                GetComponent<Archer>().SetDelegates(this, _Anim, _SelectedWeapon);
                layer = 2;
                _WeaponSlotLeft.UnSheath(weapon);
                break;
            case WeaponType.Sword:
                _WeaponSlotRight.UnSheath(weapon);
                GetComponent<Swordsman>().SetDelegates(this, _Anim, _SelectedWeapon);
                GetComponent<Swordsman>().AnimLayer = 3;
                layer = 3;
                break;
            case WeaponType.TwoHandedSword:
                _WeaponSlotRight.UnSheath(weapon);
                GetComponent<Swordsman>().SetDelegates(this, _Anim, _SelectedWeapon);
                GetComponent<Swordsman>().AnimLayer = 4;
                layer = 4;
                break;
            case WeaponType.Daggers:
                _WeaponSlotLeft.UnSheath(weapon);
                _WeaponSlotRight.UnSheath(weapon);
                GetComponent<Archer>().SetDelegates(this, _Anim, _SelectedWeapon);
                layer = 5;
                break;
        }
        for(int i = 0; i < _Anim.layerCount; i++)
        {
            _Anim.SetLayerWeight(i, 0);
            if (layer == i || i == 1) _Anim.SetLayerWeight(i, 1);
        }
    }   

    protected void Sheath()
    {
        if (!_SelectedWeapon) return;
        _CombatTime = 0;
        _SelectedWeapon.IsSheathed = false;
        _WeaponSlotLeft.Sheath();
        _WeaponSlotRight.Sheath();
        _Anim.SetTrigger("Sheath");

        for (int i = 0; i < _Anim.layerCount; i++)
        {
            _Anim.SetLayerWeight(i, 0);
            if (0 == i || 1 == i) _Anim.SetLayerWeight(i, 1);
        }
    }
}