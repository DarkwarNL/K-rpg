using UnityEngine;
using System.Collections;

public class Turn : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{

	
	}
	void OnTriggerEnter(Collider other)
	{
      
		if(other.gameObject.CompareTag("Player"))
		{

			other.GetComponent<Movement>().Getturn(gameObject);
	

		
		}
	}
}
