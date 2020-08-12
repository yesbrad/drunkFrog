using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : CharacterPawn
{
    public PlayerManager Manager { get { return manager; } }

    private PlayerManager manager;
    private Vector3 inputDirection;

    private void Awake()
    {
        manager = GetComponentInParent<PlayerManager>();
    }

	private void Update () 
	{
        UpdateInput();
	}

    private void UpdateInput ()
    {
        MoveDirection(inputDirection);
    }

    public void OnMove (InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        inputDirection.x = input.x;
        inputDirection.z = input.y;
    }

    public void OnPlaceItem (InputAction.CallbackContext context)
    {
        Manager.PlaceItem(transform.position, Manager.debugItem);
    }

    public void OnSwapItem(InputAction.CallbackContext context)
    {
        Manager.InventoryManager.ShiftItems();
    }
}
