using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player : MonoBehaviour {
    public string Name { get;  private set;}

    public void SetName(string name)
    {
        Name = name;
    }
}
