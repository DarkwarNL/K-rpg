using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CountDownGameOver : MonoBehaviour
{
	
	private int _time = 15;
	private Text _timer;
	
	void Start()
	{
		_timer = GetComponent<Text>();
		StartCoroutine(countdown());
	}
	
	IEnumerator countdown()
	{
		while (_time > 0)
		{
			_timer.text = "" + _time;
			yield return new WaitForSeconds(1);
			_time -= 1;
		}
		Application.LoadLevel(0);
	}
}