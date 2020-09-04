﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Item Data")]

[System.Serializable]
public class ItemSize
{
    [Range(1, 10)]
    public int x;

    [Range(1, 10)]
    public int y;

    public ItemSize ()
    {
        x = 1;
        y = 1;
    }

    public bool IsSingle()
    {
        return x == 1 && y == 1;
    }
}
public class ItemData : ScriptableObject
{
    public string name;
    public string id;
    public ItemSize size = new ItemSize();
    public GameObject itemPrefab;

    [Header("Debug")]
    public Color debugColor = Color.cyan;

    public ItemController SpawnController()
    {
        ItemController controller = Instantiate(itemPrefab).GetComponent<ItemController>();
        return controller;
    }
}
