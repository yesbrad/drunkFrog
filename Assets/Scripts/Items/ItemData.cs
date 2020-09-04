using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Item Data")]
public class ItemData : ScriptableObject
{
    public string name;
    public string id;
    public int size = 1;
    public GameObject itemPrefab;

    [Header("Debug")]
    public Color debugColor = Color.cyan;

    public ItemController SpawnController()
    {
        ItemController controller = Instantiate(itemPrefab).GetComponent<ItemController>();
        return controller;
    }
}
