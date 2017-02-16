using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntActivater : MonoBehaviour {
    [SerializeField]
    private Ant[] Ants;

    void OnTriggerEnter(Collider other)
    {
        VehicleController vehicle = other.GetComponent<VehicleController>();
        if (vehicle)
        {
            int i = Random.Range(0, Ants.Length);
            StartCoroutine(Ants[i].AntMove());            
        }
    }
}
