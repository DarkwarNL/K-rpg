using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour 
{
	public float speed = 5;
	public GameObject _CurrentRail;
	private GameObject _lastTurn;
	// Use this for initialization
	void Start () 
	{
		_lastTurn = gameObject;
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position += transform.forward * Time.deltaTime * speed;
		//transform.rotation = _lastTurn.transform.rotation;
		transform.rotation = Quaternion.Slerp(transform.rotation, _lastTurn.transform.rotation, Time.deltaTime * 5);


	

	}
	public void Getturn(GameObject turn)
	{
		_lastTurn = turn;

	}
}
