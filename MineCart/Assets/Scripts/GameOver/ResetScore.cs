using UnityEngine;
using System.Collections;

public class ResetScore : MonoBehaviour 
{
	public void Test()
	{
//		Debug.Log("Button was pressed");
		PlayerPrefs.DeleteAll ();
	}
}
