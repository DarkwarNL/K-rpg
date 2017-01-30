using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    /// <summary>
    /// Add in correct order!!!
    /// </summary>
    public Waypoint[] Waypoints = new Waypoint[4];

    public List<RaceData> _Players = new List<RaceData>();

    private static RaceManager _RaceManager;
    public static RaceManager Instance
    {
        get
        {
            if (!_RaceManager) _RaceManager =FindObjectOfType<RaceManager>();
            return _RaceManager;
        }
    }

    public void AddPlayer(string name)
    {
        RaceData data = new RaceData(name);
        data.LastWaypoint = Waypoints[0];
        _Players.Add(data);
    }

    public void CrossedWayPoint(string name, Waypoint waypoint)
    {        
        foreach(RaceData raceData in _Players)
        {
            if(raceData.Name == name)
            {
                if (raceData.LastWaypoint != Waypoints[Waypoints.Length- 1])
                {
                    raceData.LastWaypoint = waypoint;
                    return;
                }       

                raceData.CurrentLap++;
                if(raceData.CurrentLap >= 3)
                {
                    raceData.Completed = true;
                }

                raceData.LastWaypoint = waypoint;
            }
        }
    }

    public Waypoint GetLastWaypointByName(string name)
    {
        foreach (RaceData raceData in _Players)
        {
            if (raceData.Name == name)
            {
                return raceData.LastWaypoint;
            }
        }
        return Waypoints[0];
    }
}
