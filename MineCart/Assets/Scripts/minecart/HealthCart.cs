using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HealthCart : MonoBehaviour {

   //0 left 1 right 2 leftback 3 rightback 4 stayleft 5 stayright
    public Animator anim;
    public float hor;
	public bool hangingLeft;
    public bool hangingRight;
    private GameObject _particles;
    
    void Start()
    {
        _particles = gameObject.transform.GetChild(0).gameObject;
        anim = GetComponent<Animator>();
    }
	// Update is called once per frame
    void Update()
    {
        if (hor < 0.1 && hor > -0.1) hor = 0;
        hor = Mathf.Clamp(hor,-5,5);
        if (hor > 0)
        {
            _particles.transform.GetChild(1).gameObject.SetActive(true);
            hor -= Time.deltaTime*10;
            hangingRight = true;
        }
        else if (hor < 0)
        {
            _particles.transform.GetChild(0).gameObject.SetActive(true);
            hor += Time.deltaTime*10;
            hangingLeft = true;
        }
		else
		{
            _particles.transform.GetChild(0).gameObject.SetActive(false);
            _particles.transform.GetChild(1).gameObject.SetActive(false);
            hangingRight = false;
            hangingLeft = false;
		}
        anim.SetFloat("x", hor);
    }
    void FixedUpdate()
    {
        hor += Input.acceleration.x;

      



    }

    void GotHit()
    {
        
    }	
}
