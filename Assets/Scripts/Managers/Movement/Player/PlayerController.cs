using System.Collections;
using System.Collections.Generic;
using System.Data.OleDb;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : CharacterPawn
{
    public PlayerManager Manager { get; private set; }

    public Transform gridSelector;
    public Transform playerRotateContainer;
    private Vector3 inputDirection;

    private bool debugTime;

    public Vector3 oldPos;

    public override void Init()
    {
        base.Init();
        Manager = GetComponentInParent<PlayerManager>();
        gridSelector.parent = Manager.transform;
		Manager.SetRotationContainer(playerRotateContainer);
	}

	private void Update () 
	{
        UpdateInput();
        gridSelector.position = Vector3.Lerp(gridSelector.position, GetSelectionLocation(), Time.deltaTime * 20);
		gridSelector.eulerAngles = PencilPartyUtils.RoundAnglesToNearest90(playerRotateContainer);
		gridSelector.localScale = Vector3.Lerp(gridSelector.localScale, new Vector3(Manager.InventoryManager.currentItem.size, 1, Manager.InventoryManager.currentItem.size) * constants.GridCellSize, Time.deltaTime * 10);
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

        SetVelocity(input.sqrMagnitude);

        if(input != Vector2.zero)
            playerRotateContainer.localRotation = Quaternion.LookRotation(new Vector3(input.x, 0, input.y), Vector3.up);

    }

    private Vector3 GetSelectionLocation ()
    {
        return Manager.CurrentGrid.grid.GetWorldPositionFromWorld(transform.position + playerRotateContainer.forward * constants.GridCellSize);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube((transform.position + playerRotateContainer.forward * constants.GridCellSize), Vector3.one);
        Gizmos.DrawCube(GetSelectionLocation(), Vector3.one);

        Vector2Int a = Manager.CurrentGrid.grid.GetGridPositionFromWorld(GetSelectionLocation());

        Vector2Int[] space = Manager.CurrentGrid.grid.GetGridSpace(a.x,a.y, Manager.InventoryManager.currentItem.size, gridSelector);

        Gizmos.color = Color.red;

        for (int i = 0; i < space.Length; i++)
        {
            Gizmos.DrawCube(Manager.CurrentGrid.grid.GetWorldGridCenterPositionFromWorld(Manager.CurrentGrid.grid.GetWorldPositionFromGrid(space[i].x, space[i].y)), Vector3.one);
        }
    }

    public void OnPlaceItem (InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Manager.PlaceOrPickupCurrentItem(GetSelectionLocation());
        }
    }

    public void OnSwapItem(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //Manager.InventoryManager.ShiftItems();
        }
    }

    public void OnDebugSpawn(InputAction.CallbackContext context)
    {
            GameManager.instance.SpawnAI(Manager.HouseManager);
        if (context.performed)
        {
        }
    }

    public void OnDebugTime(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            debugTime = !debugTime;
            Time.timeScale = debugTime ? 6 : 1;
        }
    }
}
