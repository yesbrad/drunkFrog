using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<Item> currentItems = new List<Item>();

    public Item CurrentItem { get { return currentItem; } }

    private Item currentItem;

    private int selectionIndex = 0;

    private void Awake()
    {
        currentItem = currentItems[0];
    }

    public void ShiftItems ()
    {
        selectionIndex++;

        if(selectionIndex > currentItems.Count - 1)
        {
            selectionIndex = 0;
        }

        currentItem = currentItems[selectionIndex];
    }

    private void OnGUI()
    {
        GUILayout.Label(currentItem.name);
    }
}
