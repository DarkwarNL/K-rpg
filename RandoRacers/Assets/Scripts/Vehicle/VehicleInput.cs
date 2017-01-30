using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VehicleController))]
public class VehicleInput : MonoBehaviour {
    private VehicleController _Vehicle;

    void Awake()
    {
        _Vehicle = GetComponent<VehicleController>();
    }

	void FixedUpdate ()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");        
        float b = Input.GetAxis("Brake");

        _Vehicle.SetVehicleData(h, v, v, b);
    }
}
