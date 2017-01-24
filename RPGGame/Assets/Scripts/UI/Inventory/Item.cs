using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Legendary,
    Rare,
    Common,
    Normal
}

[CreateAssetMenu]
public class Item : ScriptableObject {
    public Sprite Sprite;
    public GameObject ItemObject;

    public ItemType Type;
    public int Price, MinumumPower, MaximumPower;
}
