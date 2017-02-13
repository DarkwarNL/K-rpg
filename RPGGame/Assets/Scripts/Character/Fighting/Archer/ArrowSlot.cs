using UnityEngine;
using System.Collections;

public class ArrowSlot : MonoBehaviour
{
    private Arrow _CurrentArrow = null;
    private ParticleSystem _SheathingParticle;

    void Awake()
    {
        _SheathingParticle = Instantiate(Resources.Load<GameObject>("Particles/SheathingItems"), transform, false).GetComponent<ParticleSystem>();
    }

    internal void SetArrowRotation(Vector3 target)
    {
        if(_CurrentArrow)
        {            
            _CurrentArrow.transform.LookAt(target);
        }
    }

    public void SpawnArrow(Arrow newArrow, float damage)
    {
        if (_CurrentArrow) return;

        _CurrentArrow = (Instantiate(newArrow, transform.position, Quaternion.identity, transform) as Arrow);
        
        _CurrentArrow.SetData(damage, gameObject.GetComponentInParent<Stats>().gameObject, true);
        _CurrentArrow.transform.localPosition = new Vector3(0, 0, 0.35f);
        _CurrentArrow.transform.localRotation = new Quaternion();

        _SheathingParticle.Play();
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