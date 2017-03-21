using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionBarObject : MonoBehaviour {
    private int _SkillNumber;
    [SerializeField]
    private Image _Icon;
    [SerializeField]
    private Image _CooldownImage;

    internal void SetSkillNumber(int num)
    {
        _SkillNumber = num;
    }

    internal int SetData(Sprite sprite)
    {
        _Icon.sprite = sprite;
        return _SkillNumber;
    }

    internal bool CheckSkillNumber(int num)
    {
        return num == _SkillNumber;
    }

    internal Image GetImage()
    {
        return _CooldownImage;
    }
}
