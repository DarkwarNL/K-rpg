using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Player{
    public string PlayerName;
    private Skill[] _SelectedSkills = new Skill[4];

    private static Player _Player;

    void Awake()
    {
        SaveLoad.Load();
        foreach(Player player in SaveLoad.savedPlayers)
        {
            if(player.PlayerName == PlayerName)
            {
                SetData(player);
            }
        }
    }

    void SetData(Player player)
    {
        _SelectedSkills = player._SelectedSkills;
    }

    internal Skill[] GetSelectedSkills()
    {
        return _SelectedSkills;
    }

    internal void SetSkills(Skill[] selectedSkills)
    {
        _SelectedSkills = selectedSkills;
        SaveLoad.Save(this);
    }
}
