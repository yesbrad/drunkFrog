using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InventoryManager))]
public class PlayerManager : MonoBehaviour
{
    public HouseManager houseManager;
    public Camera playerCamera;
    public Item debugItem;

    private GridController currentGrid;

    public InventoryManager InventoryManager { get { return inventoryManager; } }

    private Pawn pawn;
    private InventoryManager inventoryManager;

    private void Awake()
    {
        pawn = GetComponentInChildren<Pawn>();
        inventoryManager = GetComponent<InventoryManager>();
    }

    public void Init(HouseManager initialHouse)
    {
        SetHouse(initialHouse);
    }

    public void SetHouse (HouseManager house)
    {
        houseManager = house;
    }

    public void PlaceItem(Vector3 position, Item item)
    {
        if (currentGrid)
        {
            currentGrid.grid.PlaceItemFromPosition(position, inventoryManager.CurrentItem);
        }
    }

    private void Update()
    {
        if (!houseManager) return;

        for (int i = 0; i < houseManager.floorSettings.Length; i++)
        {
            if (houseManager.floorSettings[i].floorDetectionSocket.position.y > pawn.Position.y)
            {
                // OFF
                LayerCullingHide(playerCamera, houseManager.floorSettings[i].cameraLayer.value);

            }
            else
            {
                // ON
                LayerCullingShow(playerCamera, houseManager.floorSettings[i].cameraLayer.value);
            }
        }

        CheckForNewGrid();
    }
    public void LayerCullingShow(Camera cam, int layerMask)
    {
        cam.cullingMask |= layerMask;
    }

    public void LayerCullingHide(Camera cam, int layerMask)
    {
        cam.cullingMask &= ~layerMask;
    }

    private void CheckForNewGrid()
    {
        float minDis = Mathf.Infinity;

        // Check for current floor
        for (int i = 0; i < houseManager.gridControllers.Length; i++)
        {
            float dis = Vector3.Distance(new Vector3(pawn.Position.x, houseManager.gridControllers[i].gridOrigin.position.y, pawn.Position.z), pawn.Position + Vector3.down);
            if (dis < minDis)
            {
                currentGrid = houseManager.gridControllers[i];
                minDis = dis;
            }
        }
    }
}
