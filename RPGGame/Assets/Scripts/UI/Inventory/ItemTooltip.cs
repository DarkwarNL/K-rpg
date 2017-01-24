using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ItemTooltip : MonoBehaviour {
    private Image _Background;
    private Image _ItemImage;
    private Text _DescriptionText;
    private Item Item;

    private static ItemTooltip _ItemTooltip;

    public static ItemTooltip Instance
    {
        get
        {
            if (!_ItemTooltip) _ItemTooltip = FindObjectOfType<ItemTooltip>();
            return _ItemTooltip;
        }
    }

    private void Awake()
    {
        _Background = GetComponent<Image>();
        _ItemImage = transform.FindChild("ItemImage").GetComponent<Image>();
        _DescriptionText = transform.FindChild("ItemDescription").GetComponent<Text>();

        OnHide();
    }

    public void SetData(Item item, Vector3 pos)
    {
        transform.position = pos;
        _ItemImage.sprite = item.Sprite;
        _DescriptionText.text = item.MinumumPower + " - " + item.MaximumPower + "enter " + item.Price;

        _Background.enabled = true;
        _ItemImage.enabled = true;
        _DescriptionText.enabled = true;
    }

    public void OnHide()
    {
        _ItemImage.sprite = null;
        _DescriptionText.text = null;

        _Background.enabled = false;
        _ItemImage.enabled = false;
        _DescriptionText.enabled = false;
    }
}
