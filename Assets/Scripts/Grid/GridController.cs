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
