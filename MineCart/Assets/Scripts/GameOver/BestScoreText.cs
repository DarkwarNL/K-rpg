using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BestScoreText : MonoBehaviour 
{
	private Text _bestScore;

	public GameObject newBestScorePicture;

	void Start () 
	{
		_bestScore = GetComponent<Text>();

		if(PlayerPrefs.HasKey("Best"))
		{
			if(PlayerPrefs.GetInt("Best") == PlayerPrefs.GetInt("CurScore"))
			{
			newBestScorePicture.SetActive(true);
				_bestScore.text = PlayerPrefs.GetInt("Best").ToString();
			}
			else
			{
				//newBestScorePicture.SetActive(true);
				_bestScore.text = PlayerPrefs.GetInt("Best").ToString() ;
			}
		}
		else
		{
			_bestScore.text = "No best score yet!";
		}
	}
}