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
        Time.text = data.RaceTime.ToString();
        Rank.text = data.Rank.ToString();
    }
}
