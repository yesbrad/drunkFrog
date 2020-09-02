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

    public bool GiveItem (string itemID)
    {
        if (itemID != currentItem.id)
        {
            Debug.Log($"This is Deifferent Name: {itemID} CurrentItem: {currentItem.id}");
            return false;
        }

        for (int i = 0; i < GameManager.instance.items.Length; i++)
        {
            if (itemID == GameManager.instance.items[i].id)
            {
                currentItem = GameManager.instance.items[i];
            }
        }

        itemAmount++;
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
