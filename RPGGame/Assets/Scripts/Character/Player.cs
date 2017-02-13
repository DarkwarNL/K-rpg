using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Player{
    public string PlayerName;
    private string[] _SelectedSkills = new string[4];

    private static Player _Player;

    void Awake()
    {
        SaveLoad.Load();
        SetData(SaveLoad.GetPlayer(PlayerName));
    }

    void SetData(Player player)
    {
        _SelectedSkills = player._SelectedSkills;
    }

    internal string[] GetSelectedSkills()
    {
        return _SelectedSkills;
    }

    internal void SetSkills(string[] selectedSkills)
    {
        _SelectedSkills = selectedSkills;
        SaveLoad.Save(this);
    }
}