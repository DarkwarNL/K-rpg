using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsParticle : MonoBehaviour {
    Transform _Parent;
    ParticleSystem _Particle;


	void Start ()
    {
        _Parent = GetComponentInParent<Stats>().transform;
        _Particle = GetComponent<ParticleSystem>();
	}
	
	void FixedUpdate ()
    {
        var ma = _Particle.main;
        ma.startRotation = _Parent.rotation.y * 2;
    }
}
