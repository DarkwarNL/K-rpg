using UnityEngine;
using System.Collections;

public class Weels : MonoBehaviour {

	void Update () 
    {
        transform.Rotate(Vector3.left * (Time.deltaTime) * 1000);
	}
}
