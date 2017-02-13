using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

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
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedPlayers.gd"); //you can call it anything you want
        bf.Serialize(file, SaveLoad.savedPlayers);
        file.Close();
    }

    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/savedPlayers.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedPlayers.gd", FileMode.Open);
            SaveLoad.savedPlayers = (List<Player>)bf.Deserialize(file);
            file.Close();
        }
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