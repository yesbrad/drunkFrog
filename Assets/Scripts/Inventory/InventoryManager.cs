using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public ItemData debugStartItem;

    public Item CurrentItem { get { return currentItem; } }

    private Item currentItem;

    private PlayerManager manager;
    
    private void Start()
    {
        manager = GetComponent<PlayerManager>();
        currentItem = ItemFactory.CreateItem(debugStartItem, manager);
        RefreshUI();
    }

    public bool GiveItem (Item item)
    {
        if (CurrentItem != null)
        {
            return false;
        }

        currentItem = item;

        RefreshUI();
        return true;
    }

    public void ConsumeItem()
    {
        currentItem = null;
        RefreshUI();
    }

    public bool HasItem()
    {
        return CurrentItem != null;
    }

    public void RefreshUI()
    {
        manager.PlayerUI.SetCurrentItem(CurrentItem != null ? CurrentItem.Data.name : "");
    }
}
