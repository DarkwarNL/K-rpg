using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedObject : MonoBehaviour {
    
    void OnTriggerEnter(Collider other)
    {
        VehicleController vehicle = other.GetComponent<VehicleController>();
        if (vehicle)
        {
            vehicle.Boost(200);
        }
    }
}