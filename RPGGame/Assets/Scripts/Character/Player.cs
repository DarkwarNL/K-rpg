using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Player{
    public string PlayerName;
    public string[] SelectedSkills = new string[4];

    private static Player _Player;

    public void SetData(Player player)
    {
        SelectedSkills = player.SelectedSkills;
    }

    internal string[] GetSelectedSkills()
    {
        return SelectedSkills;
    }

    internal void SetSkills(string[] selectedSkills)
    {
        SelectedSkills = selectedSkills;
        SaveLoad.Save(this);
    }
}