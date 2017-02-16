using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour {
	public GameObject towerUpgrading;
	CreateTowers money;
	float damageUpgrade =50;
	float speedUpgrade =50;
	float rangeUpgrade =50;
	public bool enoughMoney;
    private Tower _SelectedTower;
    private static Upgrades _Upgrades;

    public static Upgrades Instance
    {
        get
        {
            if (!_Upgrades) _Upgrades = FindObjectOfType<Upgrades>();
            return _Upgrades;
        }
    }

	void Start()
	{
		money = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CreateTowers> ();
	}

    public void SetTower(Tower tower)
    {
        _SelectedTower = tower;
    }

	public void DamageUpgrade()
	{
		if(money.money>= damageUpgrade)
		{
            _SelectedTower.AddDamage(1);
		    money.money -= damageUpgrade;
	    	damageUpgrade+=50;
		}
	}
	public void SpeedUpgrade()
	{
		if (money.money>= speedUpgrade) 
		{
            _SelectedTower.AddSpeed(0.15f);
			money.money -= speedUpgrade;
			speedUpgrade+=50;
		}
	}
	public void RangeUpgrade()
	{
		if(money.money>= rangeUpgrade)
		{
            _SelectedTower.AddRange(5);
			money.money -= rangeUpgrade;
			rangeUpgrade+=50;
		}
	}
}
