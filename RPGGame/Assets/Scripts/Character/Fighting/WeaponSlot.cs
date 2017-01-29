using UnityEngine;
using System.Collections;

public class WeaponSlot : MonoBehaviour
{
    private ParticleSystem _SheathingParticle;
    private GameObject _WeaponObject;
    private MeshFilter _Filter;
    private MeshRenderer _Renderer;

    private Vector3 _BowPosition = new Vector3(-0.095f, 0.1089f, 0.1142f);
    private Quaternion _BowRotation = new Quaternion(0.4f, -0.5f, 0.7f , 0.4f);
   // private Vector3 _BladePosition = new Vector3();
   // private Quaternion _BladeRotation = new Quaternion();

    public const float WeaponShowTime = .3f;

    void Awake()
    {
        _SheathingParticle = Instantiate(Resources.Load<GameObject>("Particles/Sheathing_02"), transform, false).GetComponent<ParticleSystem>();
        _WeaponObject = new GameObject("WeaponObject");
        _WeaponObject.transform.SetParent(transform, false);

        _Filter = _WeaponObject.AddComponent<MeshFilter>();
        _Renderer =_WeaponObject.AddComponent<MeshRenderer>();
    }

    internal void Sheath()
    {
        if (!_WeaponObject) return;
        _WeaponObject.SetActive(false);
    }

    internal void UnSheath(Weapon weapon)
    {
        if (weapon == null) return;
        if(weapon.WeaponType== WeaponType.Bow)
        {
            _WeaponObject.transform.localPosition = _BowPosition;
            _WeaponObject.transform.localRotation = _BowRotation;
        }
        
        _Filter.mesh = weapon.ItemMesh;
        _Renderer.materials = weapon.Materials;
        StartCoroutine(ShowWeapon());
    }

    internal IEnumerator ShowWeapon()
    {
        _WeaponObject.SetActive(false);
        yield return new WaitForSeconds(WeaponShowTime);
        
        _WeaponObject.SetActive(true);
        _SheathingParticle.Play();
    }
}