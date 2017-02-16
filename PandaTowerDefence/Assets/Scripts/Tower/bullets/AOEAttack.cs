using UnityEngine;
using System.Collections;

public class AOEAttack : MonoBehaviour 
{
    public float Damage;
	// Use this for initialization
	//
	void OnTriggerEnter(Collider hit) 
	{
        Enemy enemy = hit.GetComponent<Enemy>();

		if(enemy)
		{
            enemy.DeltaHealth(-Damage);
			Destroy (gameObject,1);
		}
	}
}
