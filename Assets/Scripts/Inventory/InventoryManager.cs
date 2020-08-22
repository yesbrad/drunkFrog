using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Item CurrentItem { get { return currentItem; } }

    public Item currentItem;

    public int selectionIndex = 0;

    private PlayerManager manager;
    
    private int itemAmount;

    private void Awake()
    {
        currentItem = GameManager.instance.items[0];
        itemAmount = 5;
        manager = GetComponent<PlayerManager>();
        RefreshUI();
    }

    public bool GiveItem (Item item, int amount = 1)
    {
        if (HasItem() && amount > 0 && item.id != currentItem.id)
        {
            return false;
        }

        currentItem = item;
        itemAmount += amount;
        RefreshUI();
        return true;
    }

    public void ConsumeItem()
    {
        itemAmount--;

        if (itemAmount < 0)
            itemAmount = 0;

        RefreshUI();
    }

    public bool HasItem()
    {
        return itemAmount > 0;
    }

    public void RefreshUI()
    {
        manager.PlayerUI.SetCurrentItem(itemAmount > 0 ? currentItem.name + " x" + itemAmount : "");
    }
}
