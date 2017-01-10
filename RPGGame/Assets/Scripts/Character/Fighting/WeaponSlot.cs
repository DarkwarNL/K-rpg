using UnityEngine;
using System.Collections;

public class WeaponSlot : MonoBehaviour
{
    protected Weapon _CurrentWeapon = null;
    protected GameObject _SheathingParticle;

    protected GameObject _WeaponObject;

    void Awake()
    {
        _SheathingParticle = Resources.Load<GameObject>("Particles/Sheathing");
    }

    internal void Sheath()
    {
        if (!_WeaponObject) return;
        Destroy(_WeaponObject);
    }

    internal void UnSheath(Weapon nextWeapon)
    {
        if (_WeaponObject != null) return;

        _CurrentWeapon = nextWeapon;
        _WeaponObject = (GameObject)Instantiate(nextWeapon.gameObject, transform, false);

        ParticleSystem sheatingParticle = (Instantiate(_SheathingParticle, transform.position, transform.rotation, transform) as GameObject).GetComponent<ParticleSystem>();
        Destroy(sheatingParticle.gameObject, sheatingParticle.main.startLifetime.constant);
    }
}