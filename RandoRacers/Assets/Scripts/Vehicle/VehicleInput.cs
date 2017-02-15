using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VehicleController))]
public class VehicleInput : MonoBehaviour {
    private VehicleController _Vehicle;
    public const string PlayerString = "_Player";

    internal int PlayerNumber;

    void Awake()
    {
        _Vehicle = GetComponent<VehicleController>();
    }

	void FixedUpdate ()
    {
        float v = Input.GetAxis("Vertical" + PlayerString + PlayerNumber);
        float h = Input.GetAxis("Horizontal" + PlayerString + PlayerNumber);        
        float b = Input.GetAxis("Brake" + PlayerString + PlayerNumber);

        _Vehicle.SetVehicleData(h, v, v, b);

        if (Input.GetButtonDown("PauseGame"))
        {
            MainMenu.Instance.PauseGame();
        }
    }
}
