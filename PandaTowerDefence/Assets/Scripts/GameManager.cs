using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{    
    protected int _Health;
    protected float _Zen;
    protected int _Level;

    public float GetZen()
    {
        return _Zen;
    }

    public bool CheckZen(float amount)
    {
        return  amount <= _Zen;
    }

    public void DeltaZen(float amount)
    {
        _Zen -= amount;
    }

    public void DeltaHealth()
    {
        _Health--;
    }

    public int GetLevel()
    {
        return _Level;
    }
}
