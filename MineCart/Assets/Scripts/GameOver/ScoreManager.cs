using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class ScoreManager : MonoBehaviour 
{
	public int score = 0;
	public int bestScore;
    public Text score1;
    public Text score2;
    public Text score3;
    public List<AudioClip> clips;
    public bool vrMode;
	
	public bool scoreSwitch = false;

	private int _scoreMultiplyer = 1;

	// Update is called once per frame
	void Update () 
	{
		if (scoreSwitch == true&& !GameObject.FindGameObjectWithTag("Canvases").GetComponent<VRToggle>()._toggle)
		{
            score += _scoreMultiplyer;
            score1.text = score + "";
            score2.text = score + "";
            vrMode = true;
            gameObject.GetComponentInParent<MinerHealth>().vrMode = vrMode;
		}
        else if (scoreSwitch == true && GameObject.FindGameObjectWithTag("Canvases").GetComponent<VRToggle>()._toggle)
        {
            score += _scoreMultiplyer;
            score3.text = score + "";
            vrMode = false;
            gameObject.GetComponentInParent<MinerHealth>().vrMode = vrMode;
        }
	}

    void FixedUpdate()
    {
        if(score >= 1000&&score <= 5000)
        {
            Change(1);            
        }
        else if (score >= 5000 && score <= 10000)
        {
            Change(2);
        }
        else if (score >= 10000 && score <= 15000)
        {
            Change(3);
        }
        else if (score >= 15000 && score <= 20000)
        {
            Change(4);
        }
    }

    void Change(int nmbr)
    {
        if (nmbr == 1&& score <= 1010)
        {
            gameObject.transform.GetComponentInParent<Movement>().speed = 15;
        }
        else if (nmbr == 2 && score <= 5010)
        {
            gameObject.transform.GetComponentInParent<Movement>().speed = 20;
            _scoreMultiplyer = 3;
            GetComponent<AudioSource>().clip = clips[1];
            GetComponent<AudioSource>().Play();
        }
        else if (nmbr == 3 && score <= 10010)
        {
            gameObject.transform.GetComponentInParent<Movement>().speed = 23;
            _scoreMultiplyer = 4;
            GetComponent<AudioSource>().clip = clips[2];
            GetComponent<AudioSource>().Play();
        }
        else if (nmbr == 4 && score <= 15010)
        {
            gameObject.transform.GetComponentInParent<Movement>().speed = 26;
            _scoreMultiplyer = 4;
            GetComponent<AudioSource>().clip = clips[3];
            GetComponent<AudioSource>().Play();
        }
    }
}
