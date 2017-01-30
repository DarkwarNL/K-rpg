using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RaceData{
    [SerializeField]
    public string Name;
    public int Ranked;
    public int CurrentLap;
    public Waypoint LastWaypoint;
    public int RaceTime;
    public bool Completed = false;

    public RaceData(string name)
    {
        Name = name;
    }

    public RaceData()
    {

    }
}
