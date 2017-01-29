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
    public ItemType ItemType;
    public int Price, MinumumPower, MaximumPower;

    /// <summary>
    /// Spawning weapons etc.
    /// </summary>
    public Mesh ItemMesh;
    public Material[] Materials;
}
