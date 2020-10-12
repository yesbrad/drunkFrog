using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(InventoryManager))]
public class PlayerManager : CharacterManager
{
    public Camera playerCamera;
    public Item debugItem;
    [SerializeField] PlayerUI playerUI;

    public PlayerUI PlayerUI { get { return playerUI; } }
    public PlayerDetection Detection { get; private set; }
    public int Cash { get; private set; }

    public override void Awake()
    {
        base.Awake();
        playerCamera.enabled = false;
        GetComponent<PlayerInput>().DeactivateInput();
    }

    public override void Init(HouseManager initialHouse)
    {
        playerCamera.enabled = true;
        base.Init(initialHouse);
        Detection = GetComponent<PlayerDetection>();
        transform.GetComponentInChildren<PlayerController>().Spawn(initialHouse.SpawnPosition);
        GetComponent<PlayerInput>().ActivateInput();
        AddCash(CurrentHouse.BaseCash);
        Detection.Init(this);
    }

    public void Interact()
    {
        Detection.Detect();
    }

    public void AddCash(int cash)
    {
        this.Cash += cash;
        playerUI.SetCash(this.Cash);
    }

    public override void Update()
    {
        base.Update();
        if (!CurrentHouse) return;
        UpdateCameraLayer();
    }

    private void UpdateCameraLayer()
    {
        for (int i = 0; i < CurrentHouse.floorSettings.Length; i++)
        {
            if (CurrentHouse.floorSettings[i].floorDetectionSocket.position.y > Pawn.Position.y)
            {
                // OFF
                LayerCullingHide(playerCamera, CurrentHouse.floorSettings[i].cameraLayer.value);
            }
            else
            {
                // ON
                LayerCullingShow(playerCamera, CurrentHouse.floorSettings[i].cameraLayer.value);
            }
        }
    }
}
