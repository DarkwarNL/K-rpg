using UnityEngine;
using System.Collections;

public class BulletDamage : MonoBehaviour {
    public float Damage;
	public bool AOEAttack = false;
	public GameObject AOEPrefab;
	public GameObject NormalHitPrefab;
	// Use this for initialization
	void Start () 
	{
		Destroy (gameObject, 4);
	}
	// Update is called once per frame
	void Update () 
	{
		transform.Translate (Vector3.forward * 25 * Time.deltaTime);
	}

	void OnCollisionEnter(Collision hit) 
	{
        Enemy enemy = hit.transform.GetComponent<Enemy>();
        if (!enemy) return;

		if(AOEAttack == true)
		{
			var go = Instantiate(AOEPrefab,transform.position, transform.rotation);
			Destroy (gameObject);
		}
		//if colliding enemy damage him and spawn particles
		else
		{
			var obj= Instantiate(NormalHitPrefab,transform.position, transform.rotation);
            enemy.DeltaHealth(-Damage);
			Destroy (gameObject);
		}
	}
}
