using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour {
    public float Speed;
    public float DamageMultiplier =1;
    public float Damage;
    public bool Move;

	void Update ()
    {
        if(Move)
            transform.position += transform.forward * Time.deltaTime * Speed;
	}

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth health = other.GetComponent<PlayerHealth>();

        if (health)
        {
            health.DeltaHealth(Damage * DamageMultiplier);
        }
    }
}
