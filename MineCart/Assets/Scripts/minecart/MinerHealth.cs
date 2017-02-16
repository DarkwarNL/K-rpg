using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MinerHealth : MonoBehaviour
{
    public bool vrMode;
    private GameObject _canvasLeft;
    private GameObject _canvasRight;
    private GameObject _canvasNormal;
    private int _health = 3;
    private ScoreManager _scoreManagerScript;

    void Start()
    {
        _scoreManagerScript = GameObject.FindGameObjectWithTag("Manager").GetComponent<ScoreManager>();
        _canvasLeft = GameObject.FindGameObjectWithTag("LeftCanvas");
        _canvasRight = GameObject.FindGameObjectWithTag("RightCanvas");
        _canvasNormal = GameObject.FindGameObjectWithTag("CanvasNormal");
        if (_canvasNormal != null)
        {
            _canvasNormal.gameObject.SetActive(false);
        }       
    }

    void Update()
    {
        if (vrMode)
        {
            if (_health == 2)
            {
                _canvasLeft.transform.GetChild(0).gameObject.SetActive(false);
                _canvasRight.transform.GetChild(0).gameObject.SetActive(false);
            }
            else if (_health == 1)
            {
                _canvasLeft.transform.GetChild(1).gameObject.SetActive(false);
                _canvasRight.transform.GetChild(1).gameObject.SetActive(false);
            }
            else if (_health == 0)
            {
                _canvasLeft.transform.GetChild(2).gameObject.SetActive(false);
                _canvasRight.transform.GetChild(2).gameObject.SetActive(false);
                Dead();
                //game over
            }
        }
        else if (!vrMode)
        {
            if (_health == 2)
            {
                _canvasNormal.transform.GetChild(0).gameObject.SetActive(false);
            }
            else if (_health == 1)
            {
                _canvasNormal.transform.GetChild(1).gameObject.SetActive(false);
            }
            else if (_health == 0)
            {
                _canvasNormal.transform.GetChild(2).gameObject.SetActive(false);
                Dead();
                //game over
            }
        }


    }

    public void DeltaHealth()
    {
        _health--;
    }

    void Dead()
    {
        Destroy(gameObject);
        PlayerPrefs.SetInt("CurScore", _scoreManagerScript.score);
        if (PlayerPrefs.GetInt("Best") < (PlayerPrefs.GetInt("CurScore")))
        {
            PlayerPrefs.SetInt("Best", _scoreManagerScript.score);
            PlayerPrefs.Save();
            Debug.Log(PlayerPrefs.GetInt("Best").ToString() + " <- saved");
        }
        Debug.Log(PlayerPrefs.GetInt("Best").ToString() + " <- MHealth");
        _scoreManagerScript.scoreSwitch = false;
        Application.LoadLevel("MinecartGameOver");
    }

    void CheckCurBest()
    {
        if (PlayerPrefs.GetInt("Best") < (PlayerPrefs.GetInt("CurScore")))
        {
            PlayerPrefs.SetInt("Best", _scoreManagerScript.score);
            PlayerPrefs.Save();
            Debug.Log(PlayerPrefs.GetInt("Best").ToString() + " <- ScoreManager");
        }
    }
}
