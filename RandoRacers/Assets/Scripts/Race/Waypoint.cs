using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Waypoint : MonoBehaviour
{
    [SerializeField]
    private bool _FirstWaypoint;
    private RaceManager _Manager;

    void Awake()
    {
        _Manager = RaceManager.Instance;
        BoxCollider col = GetComponent<BoxCollider>();
        col.isTrigger = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        VehicleController veh = other.GetComponent<VehicleController>();
        if (veh)
        {
            _Manager.CrossedWayPoint(veh.Name, this);           
        }
    }

    public void Respawn(VehicleController vehicle)
    {
        vehicle.transform.position = transform.position;
        vehicle.transform.rotation = transform.rotation;
        vehicle.GetComponent<VehicleInput>().enabled = true;
    }
}
