using UnityEngine;
using System.Collections;

public class ArrowSlot : MonoBehaviour
{
    private Arrow _CurrentArrow = null;

    public void SpawnArrow(Arrow newArrow)
    {
        if (_CurrentArrow) return;
        _CurrentArrow = Instantiate(newArrow);
        _CurrentArrow.SetData(-10, gameObject.GetComponentInParent<Combat>().gameObject, true);
        _CurrentArrow.transform.SetParent(transform);
        _CurrentArrow.transform.localPosition = new Vector3(0, 0, 0);
        _CurrentArrow.transform.rotation = new Quaternion();
    }

    public void Release()
    {
        if (!_CurrentArrow) return;
        _CurrentArrow.transform.SetParent(null);
        _CurrentArrow.Release();
        Destroy(_CurrentArrow, 5);
        _CurrentArrow = null;
    }
}
