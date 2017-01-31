using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RaceData{
    [SerializeField]
    public string Name;
    public int Rank;
    public int CurrentLap;
    public int LastWaypoint;
    public float RaceTime;
    public bool Completed = false;
    public RaceGUI GUI;

    public bool IsBot;

    public RaceData(string name, bool bot, int rank)
    {
        Name = name;
        IsBot = bot;
        Rank = rank;
    }

    public RaceData()
    {

    }
}
