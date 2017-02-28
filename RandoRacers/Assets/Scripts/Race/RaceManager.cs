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

    private Vector3 _SpawnPoint = new Vector3(100,95,65);
    [SerializeField]
    private List<VehicleController> _Vehicles = new List<VehicleController>();
    [SerializeField]
    private List<RaceData> _VehicleData = new List<RaceData>();
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

    void FixedUpdate()
    {
        UpdateRank();
    }

    public void CreatePlayers(List<Player> players)
    {  
        foreach (Player player in players)
        {
            if (player == null) continue;
            if (player.Name == null) continue;
            AddPlayer(player.Name, false);
            Vector3 pos = new Vector3(Random.Range(90,120),105,93);
            VehicleController veh = Instantiate(Resources.Load<GameObject>("Prefabs/Vehicle"), pos, Quaternion.identity).GetComponent<VehicleController>();
            veh.SetColor(player.VehicleColor);
            veh.Name = player.Name;
            _Vehicles.Add(veh);
        }

        if (_Vehicles.Count <= 1)
        {
            CreateRaceCamera(_Vehicles[0], new Rect(0, 0, 1, 1), 0, "RaceCanvas");
        }
        else
        {
            for (int i = 0; i < 2; i++)
            {
                CreateRaceCamera(_Vehicles[i], new Rect(0, i * 0.5f, 1, 0.5f), i, "RaceCanvasSplitscreen");
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
        RaceData data = new RaceData(name, bot, _VehicleData.Count + 1);
        _VehicleData.Add(data);
        return !bot;
    }

    private void Finished()
    {
        foreach (RaceData data in _VehicleData)
        {
            if (!data.Completed) return;
        }
        _VehicleData = _VehicleData.OrderBy(x => x.Rank).ToList();
        MainMenu.Instance.OpenRaceMenu(_VehicleData);
    }

    public void CrossedWayPoint(string name, Waypoint waypoint)
    {        
        foreach(RaceData raceData in _VehicleData)
        {
            if(raceData.Name == name)
            {
                if (Waypoints[raceData.LastWaypoint] == waypoint)
                    return;

                if (raceData.LastWaypoint != Waypoints.Length- 1)
                {
                    raceData.LastWaypoint = waypoint.PointNumber;                    
                    continue;
                }       

                if(raceData.CurrentLap >= _LapCount)
                {
                    raceData.Completed = true;
                    raceData.RaceTime = Time.time - _StartTime;
                    
                    Finished();
                    continue;
                }
                raceData.CurrentLap++;
                raceData.LastWaypoint = waypoint.PointNumber;
                raceData.GUI.SetLap(raceData.CurrentLap);
            }
        }
    }

    private void UpdateRank()
    {
        if (_VehicleData.Count <= 1) return;

        int first = 0;
        int second = 1;

        for(int i =0; i < _VehicleData.Count; i++)
        {
            if(_VehicleData[i].Rank == 1)
            {
                first = i;
            }
            else
            {
                second = i;
            }
        }
        
        if (_VehicleData[second].CurrentLap > _VehicleData[first].CurrentLap)
        {
            _VehicleData[second].Rank = _VehicleData[first].Rank;
            _VehicleData[first].Rank++;
        }
        else if (_VehicleData[second].CurrentLap == _VehicleData[first].CurrentLap)
        {
            if (_VehicleData[second].LastWaypoint > _VehicleData[first].LastWaypoint)
            {
                _VehicleData[second].Rank = _VehicleData[first].Rank;
                _VehicleData[first].Rank++;
            }
            else if (_VehicleData[second].LastWaypoint == _VehicleData[first].LastWaypoint)
            {
                int waypoint = _VehicleData[first].LastWaypoint +1 ;
                if (waypoint > Waypoints.Length) waypoint = 0;

                float firstDistance = Vector3.Distance(_Vehicles[first].transform.position, Waypoints[waypoint].transform.position);
                float secondDistance = Vector3.Distance(_Vehicles[second].transform.position, Waypoints[waypoint].transform.position);

                if (secondDistance < firstDistance)
                {
                    _VehicleData[second].Rank = _VehicleData[first].Rank;
                    _VehicleData[first].Rank++;
                }
            }
        }
        _VehicleData[first].GUI.SetPositionText(_VehicleData[first].Rank);
        _VehicleData[second].GUI.SetPositionText(_VehicleData[second].Rank);
    }

    public RaceData GetRaceDataByName(string name)
    {
        foreach (RaceData raceData in _VehicleData)
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
        foreach (RaceData raceData in _VehicleData)
        {
            if (raceData.Name == name)
            {
                return Waypoints[raceData.LastWaypoint];
            }
        }
        return Waypoints[0];
    }
}
