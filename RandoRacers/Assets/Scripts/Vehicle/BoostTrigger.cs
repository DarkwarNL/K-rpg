using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class BoostTrigger : MonoBehaviour
{
    [SerializeField]
    private float _BoostAmount = 10000;

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<VehicleController>())
        {
            other.GetComponent<Rigidbody>().AddForce(other.transform.forward * _BoostAmount, ForceMode.Impulse);
        }
    }
}
