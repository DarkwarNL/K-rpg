using UnityEngine;
using System.Collections;

public class Bats : MonoBehaviour
{
    public bool leftRight; // left true right false
	void OnTriggerEnter(Collider Hit)
	{
		if(Hit.gameObject.CompareTag("Player"))
		{
            if (Hit.GetComponentInChildren<HealthCart>().hangingLeft == false && !leftRight)
			{
				Hit.GetComponent<MinerHealth>().DeltaHealth();
			}
            else if(Hit.GetComponentInChildren<HealthCart>().hangingRight == false && leftRight)
            {
                Hit.GetComponent<MinerHealth>().DeltaHealth();
            }
		}
	}
}
