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
        grid = new Grid(gridSizeX, gridSizeY, constants.GridCellSize, transform);
    }

    public bool HasItem(Vector3 position)
    {
        Vector2Int gridPosition = grid.GetGridPositionFromWorld(position);

        if (grid.IsInBounds(gridPosition.x, gridPosition.y))
        {
            return grid.GetValue(gridPosition.x, gridPosition.y) != null;
        }

        return false;
    }

    /// <summary>
    /// Place Item on Grid. Returns the new Item is it is successfuly placed.
    /// </summary>
    public Item PlaceItem(Vector3 position, Item item, CharacterManager player, bool boxed = false)
    {
        Vector2Int gridPosition = grid.GetGridPositionFromWorld(position);

        if (grid.IsInBounds(gridPosition.x, gridPosition.y))
        {
            if(grid.GetValue(gridPosition.x, gridPosition.y) == null)
            {
                ItemController cont = Instantiate(item.itemPrefab, grid.GetWorldGridCenterPositionFromWorld(position), Quaternion.identity).GetComponent<ItemController>();
                Item instanedItem = item.Init(cont, player, boxed);
                grid.SetValue(gridPosition.x, gridPosition.y, instanedItem);
                return instanedItem;
            }
        }

        return null;
    }

    /// <summary>
    /// Removes Item off the grid, Returns the Removed item
    /// </summary>
    public Item RemoveItem(Vector3 position)
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
            grid.GetValue(gridPosition.x, gridPosition.y).Interact(player.Pawn);
        }
    }

    public Item GetItem(Vector3 position)
    {
        Vector2Int gridPosition = grid.GetGridPositionFromWorld(position);

        if (grid.IsInBounds(gridPosition.x, gridPosition.y))
        {
            Item item = grid.GetValue(gridPosition.x, gridPosition.y);
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
