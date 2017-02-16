using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour 
{
	private Text _bestScore;
	
	// Use this for initialization
	void Start () 
	{
		_bestScore = GetComponent<Text>();
		
		if(PlayerPrefs.HasKey("CurScore"))
		{
			_bestScore.text = PlayerPrefs.GetInt("CurScore").ToString();
		}
		else
		{
			_bestScore.text = "No score yet!";
		}
	}
}