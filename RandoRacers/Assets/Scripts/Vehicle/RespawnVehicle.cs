using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class RespawnVehicle : MonoBehaviour {   
    
    void Awake()
    {
        BoxCollider col = GetComponent<BoxCollider>();
        col.isTrigger = true;
        col.size = new Vector3(1000, 75, 1000);
        col.center = new Vector3(250, 50, 250);
    } 

    void OnTriggerEnter(Collider other)
    {
        VehicleController veh = other.GetComponent<VehicleController>();
        if (veh)
        {
            veh.FollowingCamera.Target = null;
            Instantiate(Resources.Load<GameObject>("Prefabs/VehicleExplosion"), veh.transform,false);     
            veh.Invoke("OnDead", 2f);
        }
    }
}
