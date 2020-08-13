using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : CharacterPawn
{
    public PlayerManager Manager { get; private set; }

    public Transform gridSelector;
    public Transform playerRotateContainer;
    private Vector3 inputDirection;

    private void Awake()
    {
        Manager = GetComponentInParent<PlayerManager>();
        gridSelector.parent = Manager.transform;
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

        if(input != Vector2.zero)
            playerRotateContainer.localRotation = Quaternion.LookRotation(new Vector3(input.x, 0, input.y), Vector3.up);

        gridSelector.position = GetSelectionLocation();
    }

    private Vector3 GetSelectionLocation ()
    {
        return Manager.GetGridPosition(transform.position + playerRotateContainer.forward.normalized * 2);
    }

    public void OnPlaceItem (InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Manager.PlaceItem(GetSelectionLocation(), Manager.debugItem);
        }
    }

    public void OnSwapItem(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Manager.InventoryManager.ShiftItems();
        }
    }
}
