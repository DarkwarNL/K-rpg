using UnityEngine;
using System.Collections;
using System;
	[Serializable]
	public class Track 
{
		
	public string trackName;
	public int trackID;
	public TrackType trackType;
	public GameObject prefab;
	public float distance;
	public enum TrackType
	{
		Standart,
		CurveDown,
		CurveLeft,
		CurveRight,
		CurveUp
	}
	
	public Track(string name,float dis, int id ,TrackType type, GameObject model)
	{
		trackName = name;
		trackID = id;
		trackType = type;
		prefab = model;
		distance = dis;

	}
	
	public Track()
	{
		
	}

}
