using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class CreateTowers : MonoBehaviour {

	public float money= 250;
	private bool onplatform;
	private Vector3 placementposition;
	public Text moneyText;

	void Update()
	{
		moneyText.text =""+money;
		raytoplatform();
	}

	void raytoplatform()
	{
		RaycastHit hit;
		if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit)) 
		{
			if(hit.collider.CompareTag("TowerPlatform"))
			{
				onplatform = true;
				placementposition = hit.point;
			}
			else
			{
				onplatform = false;
			}			
		}
	}
}
