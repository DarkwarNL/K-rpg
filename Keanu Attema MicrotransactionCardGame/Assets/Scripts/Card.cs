using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum CardType
{
    Normal,
    Rare,
    Epic
}

[CreateAssetMenu]
[Serializable]
public class Card : ScriptableObject{
    public string Name;
    public CardType Type;
    public Sprite CardImage;
}
