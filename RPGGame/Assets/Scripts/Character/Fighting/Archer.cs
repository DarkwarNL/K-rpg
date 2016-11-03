using UnityEngine;
using System.Collections;

public class Archer : Combat
{
	void Update ()
    {
        if (Input.GetButton("Fire"))
        {
            _Anim.SetTrigger("OnSheet");
        }
	}
}
