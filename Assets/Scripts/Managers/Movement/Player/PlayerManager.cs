using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InventoryManager))]
public class PlayerManager : CharacterManager
{
    public Camera playerCamera;
    public Item debugItem;

    public InventoryManager InventoryManager { get; private set; }

    public override void Awake()
    {
        base.Awake();
        InventoryManager = GetComponent<InventoryManager>();
    }

    public override void Init(HouseManager initialHouse)
    {
        base.Init(initialHouse);
    }

    public void PlaceInventoryItem(Vector3 position)
    {
        PlaceOrUseItem(position, InventoryManager.CurrentItem);
    }

    public override void Update()
    {
        base.Update();
        if (!HouseManager) return;
        UpdateCameraLayer();
    }

    private void UpdateCameraLayer()
    {
        for (int i = 0; i < HouseManager.floorSettings.Length; i++)
        {
            if (HouseManager.floorSettings[i].floorDetectionSocket.position.y > Pawn.Position.y)
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
    }
}
