using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class PartsSpawner : MonoBehaviour 
{

	public int amount = 100;


	public GameObject _lastSpawnedTrackPart;
	private TracksDatabase _tracksDatabase;
	public GameObject _player;
	private List <GameObject> _spawnedTracks = new List<GameObject>();
    private bool lastUp =false;
    private bool lastDown = false;
    private bool lastLeft = false;
    private bool lastRight = false;
    private float upPos = 6.88f;
    private float downPos = -6.98f;
    private GameObject T;
	private int removecount;
	// Use this for initialization
	void Start ()
	{
		_spawnedTracks.Add (_lastSpawnedTrackPart);
		_tracksDatabase = GameObject.FindObjectOfType<TracksDatabase> ();
	}
	
	// Update is called once per frame
	void Update ()
	{

		var dis = Vector3.Distance (_lastSpawnedTrackPart.transform.position, _player.transform.position);
		if(dis < 30)
		{
			Spawn();
		}
		if(_spawnedTracks.FirstOrDefault())
		{
			var dis2 = Vector3.Distance (_spawnedTracks.FirstOrDefault().transform.position , _player.transform.position);

			if(dis2 >= 50)
			{
				print("doe die shizzle");
				var temp = _spawnedTracks.FirstOrDefault();
				Destroy(temp);
				_spawnedTracks.RemoveAt(0);


			}
		}
		
	}
	void Spawn()
	{
		if(Random.Range(0, 20) < 10||lastUp || lastDown||lastLeft||lastRight)
		{
			if (lastUp)
			{//62.65 63.645
				T = (GameObject)Instantiate(_tracksDatabase.tracks[0].prefab,
				                            (_lastSpawnedTrackPart.transform.position + _lastSpawnedTrackPart.transform.TransformDirection(new Vector3(0, upPos, _tracksDatabase.tracks[0].distance+0.905f))),
				                            _lastSpawnedTrackPart.transform.rotation);
				
				lastUp = false;
			}
			else if(lastDown)
			{
				T = (GameObject)Instantiate(_tracksDatabase.tracks[0].prefab,
				                            (_lastSpawnedTrackPart.transform.position + _lastSpawnedTrackPart.transform.TransformDirection(new Vector3(0, downPos, _tracksDatabase.tracks[0].distance))),
				                            _lastSpawnedTrackPart.transform.rotation);
				
				lastDown = false;
			}
			else if (lastLeft)
			{
				var thingy = _lastSpawnedTrackPart.transform.GetChild(1).transform.rotation;
				
				T = (GameObject)Instantiate(_tracksDatabase.tracks[0].prefab,
				                            (_lastSpawnedTrackPart.transform.position + _lastSpawnedTrackPart.transform.TransformDirection(new Vector3(-3.67f, 0.1f, _tracksDatabase.tracks[0].distance +1.5f))),
				                            thingy);
				
				lastLeft = false;
			}
			else if (lastRight)
			{
				var thingy = _lastSpawnedTrackPart.transform.GetChild(1).transform.rotation;
				
				T = (GameObject)Instantiate(_tracksDatabase.tracks[0].prefab,
				                            (_lastSpawnedTrackPart.transform.position + _lastSpawnedTrackPart.transform.TransformDirection(new Vector3(3.7f, 0f, _tracksDatabase.tracks[0].distance+1.36f))),
				                            thingy);
				
				lastRight = false;
			}
			else 
			{
				T = (GameObject)Instantiate(_tracksDatabase.tracks[0].prefab,
				                            (_lastSpawnedTrackPart.transform.position + _lastSpawnedTrackPart.transform.TransformDirection(new Vector3(0, 0, _tracksDatabase.tracks[0].distance))),
				                            _lastSpawnedTrackPart.transform.rotation);
				
			}
			_lastSpawnedTrackPart  = T ;   
			_spawnedTracks.Add(T);
			
			
		}
		else
		{
			
			int randomTrack = (int)_tracksDatabase.tracks[Random.Range(1,_tracksDatabase.tracks.Count)].trackID;
			if (_tracksDatabase.tracks[randomTrack].trackType == Track.TrackType.CurveUp)
			{
				lastUp = true;
			}
			else if (_tracksDatabase.tracks[randomTrack].trackType == Track.TrackType.CurveDown)
			{
				lastDown = true;
			}
			else if (_tracksDatabase.tracks[randomTrack].trackType == Track.TrackType.CurveLeft)
			{
				lastLeft = true;
			}
			else if (_tracksDatabase.tracks[randomTrack].trackType == Track.TrackType.CurveRight)
			{
				lastRight = true;
			}
			T = (GameObject)Instantiate(_tracksDatabase.tracks[randomTrack].prefab, (_lastSpawnedTrackPart.transform.position + _lastSpawnedTrackPart.transform.TransformDirection(new Vector3(0, 0, _tracksDatabase.tracks[randomTrack].distance))), _lastSpawnedTrackPart.transform.rotation);
			_lastSpawnedTrackPart  = T ; 
			_spawnedTracks.Add(T);
			var _turn =T.GetComponentInChildren<Turn>();
			if(_turn)
			{
				
				_lastSpawnedTrackPart = _turn.transform.parent.gameObject;
				

			}
			
		}
	}


	public void GetspawnedTrackPart(GameObject part)
	{

		 _lastSpawnedTrackPart = part;
	}
}
