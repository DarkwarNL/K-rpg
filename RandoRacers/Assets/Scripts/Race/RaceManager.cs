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
    private float _StartTime;

    private static RaceManager _RaceManager;
    public static RaceManager Instance
    {
        get
        {
            if (!_RaceManager) _RaceManager =FindObjectOfType<RaceManager>();
            return _RaceManager;
        }
    }

    void Awake()
    {
        _StartTime = Time.time;

        for (int i =0; i < Waypoints.Length; i++)
        {
            Waypoints[i].PointNumber = i;
        }

        List<VehicleController> players = new List<VehicleController>();
        VehicleController[] vehicles = FindObjectsOfType<VehicleController>();

        for(int i = 0; i < vehicles.Length; i++)
        {
            if(AddPlayer(vehicles[i].Name, vehicles[i].IsBot, i))
            {
                players.Add(vehicles[i]);
            }
        }

        if (players.Count <= 1)
        {
            CreateRaceCamera(players[0], new Rect(0, 0, 1, 1), 0, "RaceCanvas");            
        }
        else
        {
            for (int i = 0; i < 2; i++)
            {
                CreateRaceCamera(players[i], new Rect(0, i * 0.5f, 1, 0.5f), i, "RaceCanvasSplitscreen");
            }
        }
    }

    private void CreateRaceCamera(VehicleController vehicle, Rect rect, int number, string canvasName)
    {
        GameObject camera = Instantiate(Resources.Load<GameObject>("Prefabs/CameraObject"));
        camera.GetComponent<Camera>().rect = rect;
        CameraFollow cameraFollow = camera.GetComponent<CameraFollow>();
        cameraFollow.Target = vehicle.transform;
        vehicle.FollowingCamera = cameraFollow;
        vehicle.GetComponent<VehicleInput>().PlayerNumber = number;

        GameObject gui = Instantiate(Resources.Load<GameObject>("Prefabs/" + canvasName));
        Camera guiCamera = Instantiate(Resources.Load<GameObject>("Prefabs/GUICameraObject")).GetComponent<Camera>();
        guiCamera.rect = rect;
        gui.GetComponent<Canvas>().worldCamera = guiCamera;
        RaceGUI raceGUI = gui.GetComponent<RaceGUI>();
        raceGUI.Vehicle = vehicle;
        GetRaceDataByName(vehicle.Name).GUI = raceGUI;
    }

    private bool AddPlayer(string name, bool bot, int rank)
    {
        RaceData data = new RaceData(name, bot, rank + 1);
        _Players.Add(data);
        return !bot;
    }

    private void Finished(RaceData data)
    {

    }

    public void CrossedWayPoint(string name, Waypoint waypoint)
    {        
        foreach(RaceData raceData in _Players)
        {
            if(raceData.Name == name)
            {
                if (raceData.LastWaypoint != Waypoints.Length- 1)
                {
                    raceData.LastWaypoint = waypoint.PointNumber;

                    UpdateRank(raceData);
                    continue;
                }       

                if(raceData.CurrentLap >= 3)
                {
                    raceData.Completed = true;
                    raceData.RaceTime = Time.time - _StartTime;

                    UpdateRank(raceData);
                    Finished(raceData);
                    continue;
                }
                raceData.CurrentLap++;
                raceData.LastWaypoint = waypoint.PointNumber;

                UpdateRank(raceData);
            }
        }
    }

    private void UpdateRank(RaceData Racedata)
    {
        if (Racedata.Rank == 1) return;

        foreach(RaceData data in _Players)
        {
            if (data == Racedata || data.Rank < Racedata.Rank) continue;

            if(Racedata.CurrentLap > data.CurrentLap)
            {
                Racedata.Rank = data.Rank;
                data.Rank++;
            }
            else if (Racedata.CurrentLap == data.CurrentLap)
            {
                if (Racedata.LastWaypoint > data.LastWaypoint)
                {
                    Racedata.Rank = data.Rank;
                    data.Rank++;
                }
            }
            Racedata.GUI.SetPositionText(Racedata.Rank.ToString());
            data.GUI.SetPositionText(data.Rank.ToString());
        }        
    }

    public RaceData GetRaceDataByName(string name)
    {
        foreach (RaceData raceData in _Players)
        {
            if (raceData.Name == name)
            {
                return raceData;
            }
        }
        return new RaceData();
    }

    public Waypoint GetLastWaypointByName(string name)
    {
        foreach (RaceData raceData in _Players)
        {
            if (raceData.Name == name)
            {
                return Waypoints[raceData.LastWaypoint];
            }
        }
        return Waypoints[0];
    }
}
