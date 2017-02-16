using UnityEngine;
using System.Collections;

public class TDSpawner : MonoBehaviour
{
	private float CD;
	private float NextLevelCountdown;
	private float WaitTillNextSpawn;
	private int RandomChoice;
	private float random;
    private int faster= 10;
    public float addHealth;
    private Enemy[] _Enemies;

    void Start()
    {
        _Enemies = Resources.LoadAll<Enemy>("LOCATION IN RESOURCES");
    }

	void Update () 
	{

		CD += Time.deltaTime * 1;
		NextLevelCountdown -= Time.deltaTime * 1;

		if(NextLevelCountdown <= 0)
		{
            NextLevelCountdown = faster;
            faster--;
			random += 2;
			CD = 0;
		}

		if(CD <= 3)
		{            
			WaitTillNextSpawn -= 1 * Time.deltaTime;
			if(WaitTillNextSpawn <= 0)
			{
				var choice = Random.Range(0, random);
				if(choice <= 30)
				{
					RandomChoice = 0;
				}
				else if(choice <= 60)
				{
					RandomChoice = 1;
				}
				else
				{
					RandomChoice = 2;
				}
				WaitTillNextSpawn = 0.5f;
				Instantiate(_Enemies[RandomChoice].gameObject, transform.position, transform.rotation);
			}
		}
	}
	
}

