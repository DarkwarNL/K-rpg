using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingDamageManager : MonoBehaviour
{
    private FloatingDamageText[] Texts = new FloatingDamageText[5];
    private int _Current;

    private void Awake()
    {
        Texts = GetComponentsInChildren<FloatingDamageText>();
    }

    internal void ShowDMG(string amount)
    {
        Texts[_Current].PlayDMG(amount);
        _Current++;
        if (_Current >= Texts.Length)
            _Current = 0;
    }
}
