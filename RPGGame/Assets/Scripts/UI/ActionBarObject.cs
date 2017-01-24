using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionBarObject : MonoBehaviour {
    private int _SkillNumber;
    private Image _Icon;
    private Image _CooldownImage;

    void Awake()
    {
        _Icon = transform.FindChild("Icon").GetComponent<Image>();
        _CooldownImage = transform.FindChild("Cooldown").GetComponent<Image>();

    }

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
