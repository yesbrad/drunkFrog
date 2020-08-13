using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public int gridSizeX;
    public int gridSizeY;
    public Transform gridOrigin;
    public Transform lineContainer;
    public Grid grid;

    void Start()
    {
        grid = new Grid(gridSizeX, gridSizeY, constants.GridCellSize, gridOrigin.position);
    }

    public void PlaceOrUseItem(Vector3 position, Item item, PlayerManager player)
    {
        Vector2Int gridPosition = grid.GetGridPositionFromWorld(position);

        if (grid.IsInBounds(gridPosition.x, gridPosition.y))
        {
            if (grid.GetValue(gridPosition.x, gridPosition.y) != null)
            {
                grid.GetValue(gridPosition.x, gridPosition.y).Use(player.pawn);
            }
            else
            {
                item.Init(grid.GetWorldGridCenterPositionFromWorld(position), player);
                grid.SetValue(gridPosition.x, gridPosition.y ,item);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(gridOrigin.position, new Vector3(gridOrigin.position.x + gridSizeY * 2, gridOrigin.position.y, gridOrigin.position.z));
        Gizmos.DrawLine(gridOrigin.position, new Vector3(gridOrigin.position.x, gridOrigin.position.y, gridOrigin.position.z + gridSizeX * 2));
        Gizmos.DrawLine(new Vector3(gridOrigin.position.x, gridOrigin.position.y, gridOrigin.position.z + gridSizeX * 2), new Vector3(gridOrigin.position.x + gridSizeY * 2, gridOrigin.position.y, gridOrigin.position.z + gridSizeX * 2));
        Gizmos.DrawLine(new Vector3(gridOrigin.position.x + gridSizeY * 2, gridOrigin.position.y, gridOrigin.position.z), new Vector3(gridOrigin.position.x + gridSizeY * 2, gridOrigin.position.y, gridOrigin.position.z + gridSizeX * 2));
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(gridOrigin.position, new Vector3(gridOrigin.position.x + gridSizeY * 2, gridOrigin.position.y, gridOrigin.position.z));
        Gizmos.DrawLine(gridOrigin.position, new Vector3(gridOrigin.position.x, gridOrigin.position.y, gridOrigin.position.z + gridSizeX * 2));
        Gizmos.DrawLine(new Vector3(gridOrigin.position.x, gridOrigin.position.y, gridOrigin.position.z + gridSizeX * 2), new Vector3(gridOrigin.position.x + gridSizeY * 2, gridOrigin.position.y, gridOrigin.position.z + gridSizeX * 2));
        Gizmos.DrawLine(new Vector3(gridOrigin.position.x, gridOrigin.position.y, gridOrigin.position.z + gridSizeX * 2), new Vector3(gridOrigin.position.x, gridOrigin.position.y, gridOrigin.position.z + gridSizeX * 2));
        Gizmos.DrawLine(new Vector3(gridOrigin.position.x + gridSizeY * 2, gridOrigin.position.y, gridOrigin.position.z), new Vector3(gridOrigin.position.x + gridSizeY * 2, gridOrigin.position.y, gridOrigin.position.z + gridSizeX * 2));
    }
}
