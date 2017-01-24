using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Inventory : MonoBehaviour {
    public ItemSlot[] ItemSlots = new ItemSlot[NumItemSlots];
    public Item[] Items = new Item[NumItemSlots];
    private ItemTooltip ItemTooltip;

    public const int NumItemSlots = 28;

    private void Awake()
    {
        ItemTooltip = ItemTooltip.Instance;        

        for(int i = 0; i < ItemSlots.Length; i++)
        {            
            Transform newItemSlot = transform.GetChild(i);
            newItemSlot.name = "ItemSlot" + i;
            ItemSlots[i] = newItemSlot.GetComponent<ItemSlot>();
            ItemSlots[i].SetData(Items[i], this);           
        }
        gameObject.SetActive(false);
    }

    public void AddItem(Item itemToAdd)
    {
        for(int i = 0; i < Items.Length; i++)
        {
            if(Items[i] == null)
            {
                Items[i] = itemToAdd;
                ItemSlots[i].Image.sprite = itemToAdd.Sprite;
                ItemSlots[i].Image.enabled = true;
                return;
            }
        }
    }

    public void RemoveItem(Item itemToRemove)
    {
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i] == itemToRemove)
            {
                Items[i] = null;
                ItemSlots[i].Image.sprite = null;
                ItemSlots[i].Image.enabled = false;
                return;
            }
        }
    }

    public void OnHoverItemSlot(ItemSlot slot)
    {
        ItemTooltip.SetData(slot.Item, slot.transform.position);
    }

    public void OnExitItemSlot()
    {
        ItemTooltip.OnHide();
    }
}
