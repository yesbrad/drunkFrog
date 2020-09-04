using System.Collections;
using System.Collections.Generic;
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

    public Vector3 GetCenterPoint()
    {
        if(houseCenter != null)
        {
            return houseCenter.position;
        }

        Debug.LogError("House is missing center point varible!", gameObject);
        return Vector3.zero;
    }

    public GridController GetGrid(Vector3 position)
    {
        GridController controller = null;

        for (int i = 0; i < gridControllers.Length; i++)
        {
            if (gridControllers[i].IsInBorderBounds(position))
            {
                float distaneBetweenFloorHeightAndPawn = Vector3.Distance(new Vector3(position.x, gridControllers[i].transform.position.y, position.z), position);

                if (distaneBetweenFloorHeightAndPawn < 0.5f)
                {
                    controller = gridControllers[i];
                    return controller;
                }
            }
        }

        return controller;
    }
}
