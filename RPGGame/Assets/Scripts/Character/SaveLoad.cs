using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public static class SaveLoad
{
    public static List<Player> savedPlayers = new List<Player>();

    //it's static so we can call it from anywhere
    public static void Save(Player player)
    {      
        if(savedPlayers.Count > 0)
        {
            for (int i = 0; i < savedPlayers.Count; i++)
            {
                if (SaveLoad.savedPlayers[i].PlayerName == player.PlayerName)
                {
                    SaveLoad.savedPlayers[i] = player;
                }
                else
                {
                    SaveLoad.savedPlayers.Add(player);
                }
            }
        }
        else
        {
            SaveLoad.savedPlayers.Add(player);
        }

        string json = JsonHelper.ToJson(savedPlayers.ToArray());
        Debug.Log(json);
        PlayerPrefs.SetString("PlayersJson", json);
    }

    public static void Load()
    {
        string newJson = PlayerPrefs.GetString("PlayersJson");
        savedPlayers = JsonHelper.FromJson<Player>(newJson).ToList();
    }

    public static Player GetPlayer(string name)
    {
        foreach(Player player in savedPlayers)
        {
            if(player.PlayerName == name)
            {
                return player;
            }
        }
        return new Player();
    }
}



public class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}