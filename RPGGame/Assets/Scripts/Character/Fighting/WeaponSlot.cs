using UnityEngine;
using System.Collections;

public class WeaponSlot : MonoBehaviour
{
    protected Weapon _CurrentWeapon = null;
    protected GameObject _CurrentObject;

    public bool CanSwitch(Weapon nextWeapon)
    {
        if(_CurrentWeapon != nextWeapon)
        {            
            Switch(nextWeapon);       
            return true;
        }
        return false;
    }

    public void Sheet()
    {
        Destroy(_CurrentObject);
    }

    public void UnSheet()
    {
        Switch(_CurrentWeapon);
    }

    protected void Switch(Weapon nextWeapon)
    {
        if(_CurrentObject)
            Destroy(_CurrentObject);

        _CurrentWeapon = nextWeapon;
        _CurrentObject = Instantiate(_CurrentWeapon.gameObject);
        _CurrentObject.transform.SetParent(transform);
        _CurrentObject.transform.localPosition = new Vector3(0,0,0);
        _CurrentObject.transform.rotation = new Quaternion();
    }
}