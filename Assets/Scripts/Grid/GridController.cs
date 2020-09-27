using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public int gridSizeX;
    public int gridSizeY;
    
    [SerializeField] public Grid grid;

    public void RepaintGrid()
    {
        grid = new Grid(gridSizeX, gridSizeY, transform);
    }

    public bool HasItem(Vector3 position)
    {
        GridPosition gridPosition = grid.GetGridPositionFromWorld(position);

        if (grid.IsInBounds(gridPosition.x, gridPosition.y))
        {
            return grid.GetValue(gridPosition.x, gridPosition.y).gridState == GridSlotState.Occupied ||
                grid.GetValue(gridPosition.x, gridPosition.y).gridState == GridSlotState.Static;
        }

        return false;
    }

    /// <summary>
    /// Place Item on Grid. Returns the new Item is it is successfuly placed.
    /// </summary>
    public bool PlaceItem(Vector3 position, Item item, Transform rotationTransform)
    {
        GridPosition gridPosition = grid.GetGridPositionFromWorld(position);

		if(grid.CanPlaceItemWithSize(gridPosition.x, gridPosition.y, item.Data.size, rotationTransform))
		{
            item.OnPlace(grid.GetWorldGridCenterPositionFromWorld(position), rotationTransform != null ? Quaternion.Euler(PencilPartyUtils.RoundAnglesToNearest90(rotationTransform)) : Quaternion.identity);
			grid.SetValue(gridPosition.x, gridPosition.y, item, item.Data.size, item.Data.gridSlot, rotationTransform);
			return true;
		}
		else
		{
			Debug.Log("She does not fit on the grid");
		}

        return false;
    }

    public void OccupieSpace(Vector3 position, ItemSize size, Transform rotationTransform)
    {
        GridPosition gridPosition = grid.GetGridPositionFromWorld(position);
        grid.SetBlocked(gridPosition.x, gridPosition.y, size, rotationTransform);

    }

    /// <summary>
    /// Removes Item off the grid, Returns the Removed item
    /// </summary>
    public Item DeleteItem(Vector3 position, HouseManager manager, bool hardDelete = false)
    {
        GridPosition gridPosition = grid.GetGridPositionFromWorld(position);

        if (HasItem(position))
        {      
            return grid.DeleteValue(gridPosition.x, gridPosition.y, manager, hardDelete); ;
        }

        return null;
    }

    /// <summary>
    /// Interact with an item of the grid
    /// </summary>
    public void InteractItem(Vector3 position, CharacterManager player)
    {
        GridPosition gridPosition = grid.GetGridPositionFromWorld(position);

        if (HasItem(position))
        {
            if(grid.GetValue(gridPosition.x, gridPosition.y).item.controller.IsFull() == false)
            {
                grid.GetValue(gridPosition.x, gridPosition.y).item.Interact(player, () => { });
            }
        }
    }

    public Item GetItem(Vector3 position)
    {
        GridPosition gridPosition = grid.GetGridPositionFromWorld(position);

        if (grid.IsInBounds(gridPosition.x, gridPosition.y))
        {
            Item item = grid.GetValue(gridPosition.x, gridPosition.y).item;
            return item ?? null;
        }

        return null;
    }

    public bool IsInBounds(Vector3 position)
    {
        GridPosition gridPosition = grid.GetGridPositionFromWorld(position);
        return grid.IsInBounds(gridPosition.x, gridPosition.y);
    }

    public bool IsInBorderBounds(Vector3 position)
    {
        GridPosition gridPosition = grid.GetGridPositionFromWorld(position);
        return grid.IsInBorderBounds(gridPosition.x, gridPosition.y);
    }

    public Vector3 GetOpenRandomPosition ()
    {
        Vector3 pos = grid.GetOpenRandomPosition();
        return pos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 1);
    }
}
