using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.Linq;

public class Player : MonoBehaviour {    
    public static List<Card> SavedCards = new List<Card>();

    //it's static so we can call it from anywhere
    public static void Save(List<Card> cards)
    {
        if (cards == null) return;

        string json = JsonArray.ToJson(cards.ToArray());
        PlayerPrefs.SetString("CardsJson", json);        
    }

    public static List<Card> Load()
    {
        string newJson = PlayerPrefs.GetString("CardsJson");

        if(newJson != string.Empty)
            SavedCards = JsonArray.FromJson<Card>(newJson).ToList();

        return Player.SavedCards;
    }
}

public class JsonArray
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

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}
