using System.Collections;
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
    public Transform houseCenter;

    public List<CharacterManager> guests = new List<CharacterManager>();

    public PlayerManager houseOwner;

    public List<Item> houseInventory = new List<Item>();

    public int PP;

    public void Init (PlayerManager owner)
    {
        houseOwner = owner;
        PP = 1;
        RefreshUI();
    }

    public void AddHP (int amount)
    {
        PP += amount;
        RefreshUI();
    }

    public void RefreshUI ()
    {   
        if(houseOwner != null)
            houseOwner.PlayerUI.SetPP(GameManager.instance.CalculatePP(PP));
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

    public void PlaceOrPickupItem (GridController controller, Vector3 position, Item item, CharacterManager player)
    {
        if(controller.IsInBounds(position))
        {
            Item placedItem = PlaceItem(controller, position, item, player);

            if(placedItem == null)
            {
                Item removedItem = controller.RemoveItem(position);
                RemoveFromInventory(removedItem);
            }
        }
        else
        {
            Debug.Log("Out of Bounds");
        }
    }

    /// <summary>
    /// Places a New Item on a grid, Returns the placed item
    /// </summary>
    public Item PlaceItem (GridController controller, Vector3 position, Item item, CharacterManager player)
    {
        Item newItem = controller.PlaceItem(position, item, player);
        //Debug.Log($"Initiated Item Placed: {newItem.UUID}. Inititated Item Controller: {newItem.controller.gameObject.name}");

        if(newItem != null)
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

    public void RemoveFromInventory(Item item)
    {
        houseInventory.Remove(item);
    }

    public Vector3 GetCenterPoint()
    {
        if(houseCenter != null)
        {
            return houseCenter.position;
        }

        Debug.LogError("House is missing center point varible!", gameObject);
        return Vector3.zero;
    }
}
