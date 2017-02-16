using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Warning : MonoBehaviour {
    public Image _warning1;
    public Image _warning2;
    public Image _warning3;
    private bool _fadeIn1;
    private bool _fadeOut1;
    private bool _fadeIn2;
    private bool _fadeOut2;
    private float _fadeNumber;
    private float _fadeNumber2;
    public bool vrMode=true;

    void Start()
    {
        if (vrMode)
        {
            vrMode = GameObject.FindGameObjectWithTag("Manager").GetComponent<ScoreManager>().vrMode;
        }
        

;
    }

	void Update () 
    {
        if (vrMode)
        {
            VRFade();
        }
        else if(!vrMode)
        {
            Fade();
        }
	}

    void OnTriggerEnter(Collider Hit)
    {
        if (Hit.gameObject.CompareTag("Player"))
        {
            if (vrMode)
            {
                _warning2 = GameObject.FindGameObjectWithTag("Warning2").GetComponent<Image>();
                _warning3 = GameObject.FindGameObjectWithTag("Warning3").GetComponent<Image>();
                _fadeIn2 = true;                
            }
            else if (!vrMode)
            {
                _warning1 = GameObject.FindGameObjectWithTag("Warning1").GetComponent<Image>();
                _fadeIn1 = true;
            }
        }
    }
    void Fade()
    {
        if (_fadeIn1)
        {
            Color c = _warning1.color;
            c.a = Mathf.Lerp(_warning1.color.a, 1f, Time.deltaTime * 3);
            _warning1.color = c;
            _fadeNumber = c.a;

            if (_fadeNumber >= 0.9f)
            {
                _fadeOut1 = true;
                _fadeIn1 = false;
            }
        }
        else if (_fadeOut1)
        {
            Color c = _warning1.color;
            c.a = Mathf.Lerp(_warning1.color.a, 0f, Time.deltaTime * 5);
            _warning1.color = c;
            _fadeNumber = c.a;

            if (_fadeNumber <= 0.1f)
            {
                c.a = 0;
                _warning1.color = c;
                _fadeOut1 = false;
            }
        }
    }

    void VRFade()
    {
        if (_fadeIn2)
        {
            Color c = _warning2.color;
            c.a = Mathf.Lerp(_warning2.color.a, 1f, Time.deltaTime * 3);
            _warning2.color = c;
            _fadeNumber = c.a;

            if (_fadeNumber >= 0.9f)
            {
                _fadeOut2 = true;
                _fadeIn2 = false;
            }


            Color a = _warning3.color;
            a.a = Mathf.Lerp(_warning3.color.a, 1f, Time.deltaTime * 3);
            _warning3.color = a;
            _fadeNumber2 = a.a;

            if (_fadeNumber2 >= 0.9f)
            {
                _fadeOut2 = true;
                _fadeIn2 = false;
            }
        }
        else if (_fadeOut2)
        {
            Color c = _warning2.color;
            c.a = Mathf.Lerp(_warning2.color.a, 0f, Time.deltaTime * 5);
            _warning2.color = c;
            _fadeNumber = c.a;

            if (_fadeNumber <= 0.1f)
            {
                c.a = 0;
                _warning2.color = c;
                _fadeOut2 = false;
            }

            Color a = _warning3.color;
            c.a = Mathf.Lerp(_warning3.color.a, 0f, Time.deltaTime * 5);
            _warning3.color = a;
            _fadeNumber2 = a.a;

            if (_fadeNumber2 <= 0.1f)
            {
                a.a = 0;
                _warning3.color = c;
                _fadeOut2 = false;
            }
        }
    }
}
