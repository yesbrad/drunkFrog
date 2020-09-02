using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public int gridSizeX;
    public int gridSizeY;
    public Transform gridOrigin;
    public Transform lineContainer;
    [SerializeField] public Grid grid;

    public void InitGrid()
    {
        grid = new Grid(gridSizeX, gridSizeY, transform);
    }

    public bool HasItem(Vector3 position)
    {
        Vector2Int gridPosition = grid.GetGridPositionFromWorld(position);

        if (grid.IsInBounds(gridPosition.x, gridPosition.y))
        {
            return grid.GetValue(gridPosition.x, gridPosition.y).gridState == GridSlotState.Occupied;
        }

        return false;
    }

    /// <summary>
    /// Place Item on Grid. Returns the new Item is it is successfuly placed.
    /// </summary>
    public bool PlaceItem(Vector3 position, Item item, CharacterManager player, bool boxed = false)
    {
        Vector2Int gridPosition = grid.GetGridPositionFromWorld(position);

		if(grid.CanPlaceItemWithSize(gridPosition.x, gridPosition.y, item.size, player.RotationContainer))
		{
            ItemController cont = Instantiate(item.itemPrefab, grid.GetWorldPositionFromWorld(position), Quaternion.Euler(PencilPartyUtils.RoundAnglesToNearest90(player.RotationContainer))).GetComponent<ItemController>();
			Item instanedItem = item.Init(cont, player, boxed);
			grid.SetValue(gridPosition.x, gridPosition.y, instanedItem, GridSlotState.Occupied, instanedItem.size, player.RotationContainer);
			return true;
		}
		else
		{
			Debug.Log("She does not fit on the grid");
		}

        return false;
    }

    /// <summary>
    /// Removes Item off the grid, Returns the Removed item
    /// </summary>
    public string RemoveItem(Vector3 position)
    {
        Vector2Int gridPosition = grid.GetGridPositionFromWorld(position);

        if (HasItem(position))
        {      
            return grid.DeleteValue(gridPosition.x, gridPosition.y); ;
        }

        return null;
    }

    /// <summary>
    /// Interact with an item of the grid
    /// </summary>
    public void UseItem(Vector3 position, CharacterManager player)
    {
        Vector2Int gridPosition = grid.GetGridPositionFromWorld(position);

        if (HasItem(position))
        {
            grid.GetValue(gridPosition.x, gridPosition.y).item.Interact(player.Pawn);
        }
    }

    public Item GetItem(Vector3 position)
    {
        Vector2Int gridPosition = grid.GetGridPositionFromWorld(position);

        if (grid.IsInBounds(gridPosition.x, gridPosition.y))
        {
            Item item = grid.GetValue(gridPosition.x, gridPosition.y).item;
            return item ?? null;
        }

        return null;
    }

    public bool IsInBounds(Vector3 position)
    {
        Vector2Int gridPosition = grid.GetGridPositionFromWorld(position);
        return grid.IsInBounds(gridPosition.x, gridPosition.y);
    }

    public Item GetRandomItem ()
    {
        return grid.GetRandomItem();
    }

    public Vector3 GetRandomPosition ()
    {
        return grid.GetRandomPosition();
    }
}
