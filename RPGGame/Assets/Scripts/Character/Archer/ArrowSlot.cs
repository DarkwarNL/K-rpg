using UnityEngine;
using System.Collections;

public class ArrowSlot : MonoBehaviour
{
    private Arrow _CurrentArrow = null;

    internal void SetArrowRotation(Vector3 target)
    {
        if(_CurrentArrow&& target != null)
        {            
            _CurrentArrow.transform.LookAt(target);
        }
    }

    public void SpawnArrow(Arrow newArrow, float damage)
    {
        if (_CurrentArrow) return;
        _CurrentArrow = Instantiate(newArrow);
        _CurrentArrow.SetData(damage, gameObject.GetComponentInParent<Combat>().gameObject, true);
        _CurrentArrow.transform.SetParent(transform);
        _CurrentArrow.transform.localPosition = new Vector3(0, 0, 0);
        _CurrentArrow.transform.rotation = new Quaternion();
    }

    public void Shoot()
    {
        if (!_CurrentArrow) return;
        _CurrentArrow.transform.SetParent(null);
        _CurrentArrow.Release();
        Destroy(_CurrentArrow, 5);
        _CurrentArrow = null;
    }

    public void Release()
    {
        if (!_CurrentArrow) return;
        Destroy(_CurrentArrow.gameObject, 1);
        _CurrentArrow = null;
    }
}
