using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InventoryManager))]
public class PlayerManager : CharacterManager
{
    public Camera playerCamera;
    public Item debugItem;
    [SerializeField] PlayerUI playerUI;

    public PlayerUI PlayerUI { get { return playerUI; } }
    public PlayerDetection Detection { get; private set; }

    public override void Init(HouseManager initialHouse)
    {
        base.Init(initialHouse);
        Detection = GetComponent<PlayerDetection>();
        Pawn.SetPosition(initialHouse.SpawnPosition);
    }

    public override void Interact(Vector3 position)
    {
        base.Interact(position);
        Detection.Detect(this);
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
