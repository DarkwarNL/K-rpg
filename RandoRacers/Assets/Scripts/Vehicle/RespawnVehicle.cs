using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class RespawnVehicle : MonoBehaviour {   
    
    void Awake()
    {
        BoxCollider col = GetComponent<BoxCollider>();
        col.isTrigger = true;
        col.size = new Vector3(500, 50, 500);
        col.center = new Vector3(250, 25, 250);
    } 

    void OnTriggerEnter(Collider other)
    {
        VehicleController veh = other.GetComponent<VehicleController>();
        if (veh)
        {
            veh.Invoke("OnDead", 1.5f);
        }
    }
}
