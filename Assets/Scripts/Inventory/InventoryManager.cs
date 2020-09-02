using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Item CurrentItem { get { return currentItem; } }

    public Item currentItem;

    public int selectionIndex = 0;

    private PlayerManager manager;
    
    private void Start()
    {
        manager = GetComponent<PlayerManager>();
        ItemController cont = Instantiate(GameManager.instance.items[0].itemPrefab, Vector3.zero, Quaternion.Euler(PencilPartyUtils.RoundAnglesToNearest90(manager.RotationContainer))).GetComponent<ItemController>();

        Debug.Log($"YEmanager: {manager == null}");
        currentItem = GameManager.instance.items[0].Init(cont, manager, true);
        RefreshUI();
    }

    // TO give the old item back

    public bool GiveItem (Item item)
    {
        if (currentItem != null)
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
        return currentItem != null;
    }

    public void RefreshUI()
    {
        manager.PlayerUI.SetCurrentItem(currentItem != null ? currentItem.name : "");
    }
}
