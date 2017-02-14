using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class RaceManager : MonoBehaviour
{    
    /// <summary>
    /// Add in correct order!!!
    /// </summary>
    [SerializeField]
    private Waypoint[] Waypoints = new Waypoint[4];

    private Vector3 _SpawnPoint = new Vector3(100,100,65);

    private List<RaceData> _Players = new List<RaceData>();
    private int _LapCount;
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
    }

    public void CreatePlayers(List<Player> players)
    {
        List<VehicleController> vehicles = new List<VehicleController>();        
        foreach (Player player in players)
        {
            if (player == null) continue;
            if (player.Name == null) continue;
            AddPlayer(player.Name, false);

            VehicleController veh = Instantiate(Resources.Load<GameObject>("Prefabs/Vehicle"), _SpawnPoint* Random.Range(1.1f,1.2f), Quaternion.identity).GetComponent<VehicleController>();
            veh.SetColor(player.VehicleColor);
            veh.Name = player.Name;
            vehicles.Add(veh);
        }

        if (vehicles.Count <= 1)
        {
            CreateRaceCamera(vehicles[0], new Rect(0, 0, 1, 1), 0, "RaceCanvas");
        }
        else
        {
            for (int i = 0; i < 2; i++)
            {
                CreateRaceCamera(vehicles[i], new Rect(0, i * 0.5f, 1, 0.5f), i, "RaceCanvasSplitscreen");
            }
        }
    }

    public void SetLapCount(int count)
    {
        _LapCount = count;
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

        raceGUI.SetPositionText(number + 1);
        raceGUI.SetLap(0);
    }

    private bool AddPlayer(string name, bool bot)
    {
        RaceData data = new RaceData(name, bot, _Players.Count + 1);
        _Players.Add(data);
        return !bot;
    }

    private void Finished()
    {
        foreach (RaceData data in _Players)
        {
            if (!data.Completed) return;
        }
        _Players = _Players.OrderBy(x => x.Rank).ToList();
        MainMenu.Instance.OpenRaceMenu(_Players);
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

                if(raceData.CurrentLap >= _LapCount)
                {
                    raceData.Completed = true;
                    raceData.RaceTime = Time.time - _StartTime;

                    UpdateRank(raceData);
                    Finished();
                    continue;
                }
                raceData.CurrentLap++;
                raceData.LastWaypoint = waypoint.PointNumber;
                raceData.GUI.SetLap(raceData.CurrentLap);
                UpdateRank(raceData);
            }
        }
    }

    private void UpdateRank(RaceData Racedata)
    {
        if (Racedata.Rank == 1) return;

        foreach(RaceData data in _Players)
        {
            if (data == Racedata || data.Rank > Racedata.Rank) continue;

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
            Racedata.GUI.SetPositionText(Racedata.Rank);
            data.GUI.SetPositionText(data.Rank);
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
