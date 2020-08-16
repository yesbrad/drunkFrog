﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditorInternal.VersionControl;
using UnityEngine;

public class HouseManager : MonoBehaviour
{
    [System.Serializable]
    public struct FloorSettings
    {
        public GameObject floorPrefab;
        public Transform floorDetectionSocket;
        public LayerMask cameraLayer;
    }

    public GridController[] gridControllers;
    public FloorSettings[] floorSettings;

    public List<CharacterManager> guests = new List<CharacterManager>();

    public PlayerManager houseOwner;

    public List<Item> houseInventory = new List<Item>();

    public int HP;

    public void Init (PlayerManager owner)
    {
        houseOwner = owner;
    }

    public void AddHP (int amount)
    {
        HP += amount;
    }

    public Item GetRandomItem ()
    {
        if (houseInventory.Count <= 0)
            return null;

        int newIndex = Random.Range(0, houseInventory.Count);

        Item newItem = houseInventory[newIndex];
        
        //Debug.Log($"House Items: {newItem.UUID} : House Item Count {houseInventory.Count} : House Random Index: {newIndex}");

        return newItem;
    }

    public void PlaceOrUseItem (GridController controller, Vector3 position, Item item, CharacterManager player)
    {
        Item placedItem = PlaceItem(controller, position, item, player);

        if (placedItem != null)
        {
            UseItem(controller, position, player);
        }
    }

    public Item PlaceItem (GridController controller, Vector3 position, Item item, CharacterManager player)
    {
        Item newItem = controller.PlaceItem(position, item, player);
        Debug.Log($"Initiated Item Placed: {newItem.UUID}. Inititated Item Controller: {newItem.controller.gameObject.name}");
        AddToInventory(newItem);
        return newItem;
    }

    public void UseItem(GridController controller, Vector3 position, CharacterManager player)
    {
        controller.UseItem(position, player);
    }

    public void AddToInventory (Item item)
    {
        houseInventory.Add(item);
    }
}
