using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class WeaponTabUI : MonoBehaviour {
    private Image[] _Weapons;
    private Image _SelectedImage;
    private Color _SelectColor = Color.white;
    private Color _DeselectColor = new Color(0,0,0,0);

    private static WeaponTabUI _WeaponTabUI;

    public static WeaponTabUI Instance
    {
        get
        {
            if (!_WeaponTabUI) _WeaponTabUI = FindObjectOfType<WeaponTabUI>();
            return _WeaponTabUI;
        }
    }

	void Start ()
    {
        List<Image> images = new List<Image>();
        foreach (Transform child in transform)
        {
            images.Add(child.GetComponent<Image>());
        }
        _Weapons = images.ToArray();
	}

    public void SelectedWeapon(WeaponType type)
    {        
        for(int i = 0; i < _Weapons.Length; i++)
        {
            if((int)type == i)
            {
                if (_SelectedImage)
                {
                    _SelectedImage.color = _DeselectColor;
                }
                _SelectedImage = _Weapons[i];
                _SelectedImage.color = _SelectColor;
            }           
        }
    }
}
