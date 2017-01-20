using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionBarObject : MonoBehaviour {
    private Image _Icon;
    private Image _CooldownImage;

    void Awake()
    {
        _Icon = transform.FindChild("Icon").GetComponent<Image>();
        _CooldownImage = transform.FindChild("Cooldown").GetComponent<Image>();

    }

    public Image SetData(Sprite sprite)
    {
        _Icon.sprite = sprite;
        return _CooldownImage;
    }
}
