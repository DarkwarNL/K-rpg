using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RaceGUI : MonoBehaviour {
    [SerializeField]
    private Text _RaceTimeText;
    [SerializeField]
    private Text _PositionText;
    [SerializeField]
    private Text _SpeedText;
    [SerializeField]
    private Text _LapText;

    private float _Timer;

    internal VehicleController Vehicle;

    void LateUpdate()
    {
        _SpeedText.text = Vehicle.CurrentSpeed.ToString("00") + "Kmpu";

        _Timer += Time.deltaTime;
        string minutes = Mathf.Floor(_Timer / 60).ToString("00");
        string seconds = Mathf.Floor(_Timer % 60).ToString("00");
        _RaceTimeText.text = minutes + ":" + seconds;
    }

    public void SetPositionText(int text)
    {
        _PositionText.text = "<b>" + text + "</b>" + " th";
    }

    public void SetLap(int text)
    {
        _LapText.text =  "Lap " + "<b>"+text+"</b>";
    }
}
