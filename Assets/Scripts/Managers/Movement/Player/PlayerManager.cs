using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InventoryManager))]
public class PlayerManager : MonoBehaviour
{
    public Camera playerCamera;
    public Item debugItem;

    private GridController currentGrid;

    public InventoryManager InventoryManager { get; private set; }
    public HouseManager HouseManager { get; private set; }

    internal Pawn pawn;

    private void Awake()
    {
        pawn = GetComponentInChildren<Pawn>();
        InventoryManager = GetComponent<InventoryManager>();
    }

    public void Init(HouseManager initialHouse)
    {
        SetHouse(initialHouse);
    }

    public void SetHouse (HouseManager house)
    {
        HouseManager = house;
    }

    public void PlaceItem(Vector3 position, Item item)
    {
        if (currentGrid)
        {
            currentGrid.PlaceOrUseItem(position, InventoryManager.CurrentItem, this);
        }
    }

    public Vector3 GetGridPosition(Vector3 lookPosition)
    {
        return currentGrid.grid.GetWorldGridCenterPositionFromWorld(lookPosition);
    }

    private void Update()
    {
        if (!HouseManager) return;

        for (int i = 0; i < HouseManager.floorSettings.Length; i++)
        {
            if (HouseManager.floorSettings[i].floorDetectionSocket.position.y > pawn.Position.y)
            {
                // OFF
                LayerCullingHide(playerCamera, HouseManager.floorSettings[i].cameraLayer.value);

            }
            else
            {
                // ON
                LayerCullingShow(playerCamera, HouseManager.floorSettings[i].cameraLayer.value);
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
        for (int i = 0; i < HouseManager.gridControllers.Length; i++)
        {
            float dis = Vector3.Distance(new Vector3(pawn.Position.x, HouseManager.gridControllers[i].gridOrigin.position.y, pawn.Position.z), pawn.Position + Vector3.down);
            if (dis < minDis)
            {
                currentGrid = HouseManager.gridControllers[i];
                minDis = dis;
            }
        }
    }
}
