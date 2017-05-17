using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPad : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        CharacterMovement move = other.GetComponent<CharacterMovement>();
        if (move)
        {
            move.AddForce();
        }
    }
}
