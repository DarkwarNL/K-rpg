using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    internal Item Item;
    internal Image Image;
    private Inventory Inventory;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(Item != null)
            Inventory.OnHoverItemSlot(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(Item != null)
            Inventory.OnExitItemSlot();
    }

    public void SetData(Item item, Inventory inventory)
    {
        Inventory = inventory;
        Item = item;
        if(Item != null)
        {
            Image.sprite = item.Sprite;
            Image.enabled = true;
        }
    }

    private void Awake()
    {
        Image = transform.FindChild("ItemImage").GetComponent<Image>();
    }
}
