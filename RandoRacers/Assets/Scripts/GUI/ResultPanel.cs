using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultPanel : MonoBehaviour {

    [SerializeField]
    private Text Name;
    [SerializeField]
    private Text Time;
    [SerializeField]
    private Text Rank;

    public void SetData(RaceData data)
    {
        Name.text = data.Name;
        string minutes = Mathf.Floor(data.RaceTime / 60).ToString("00");
        string seconds = Mathf.Floor(data.RaceTime % 60).ToString("00");

        Time.text = minutes+ ":" + seconds ;
        Rank.text = data.Rank.ToString();
    }
}
