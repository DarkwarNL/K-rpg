using UnityEngine;
using System.Collections;

public class ArrowSlot : MonoBehaviour
{
    private Arrow _CurrentArrow = null;
    private GameObject _WieldArrow;

    void Awake()
    {
        _WieldArrow = Resources.Load<GameObject>("Particles/Sheathing");
    }

    internal void SetArrowRotation(Vector3 target)
    {
        if(_CurrentArrow && target != null)
        {            
            _CurrentArrow.transform.LookAt(target);
        }
    }

    public void SpawnArrow(Arrow newArrow, float damage)
    {
        if (_CurrentArrow) return;

        _CurrentArrow = (Instantiate(newArrow, transform.position, Quaternion.identity, transform) as Arrow);
        _CurrentArrow.SetData(damage, gameObject.GetComponentInParent<Combat>().gameObject, true);
        _CurrentArrow.transform.localPosition = new Vector3(0, 0, 0.35f);
        _CurrentArrow.transform.localRotation = new Quaternion();

        ParticleSystem sheatingParticle = (Instantiate(_WieldArrow, transform.position, transform.rotation, transform) as GameObject).GetComponent<ParticleSystem>();
        Destroy(sheatingParticle.gameObject, sheatingParticle.startLifetime);
    }

    public void Shoot()
    {
        if (!_CurrentArrow) return;
        _CurrentArrow.transform.SetParent(null);
        _CurrentArrow.Release();
        _CurrentArrow = null;
    }

    public void Release()
    {
        if (!_CurrentArrow) return;
        Destroy(_CurrentArrow.gameObject, 1);
        _CurrentArrow = null;
    }
}