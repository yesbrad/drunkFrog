using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public int gridSize;
    public Transform gridOrigin;
    public Transform lineContainer;
    public Grid grid;

    void Start()
    {
        grid = new Grid(gridSize, gridSize, constants.GridCellSize, gridOrigin.position);
        CreateVisualGrid();
    }

    void CreateVisualGrid ()
    {
        for (int i = 0; i < Mathf.FloorToInt(gridSize); i++)
        {
            LineRenderer renderer = Instantiate(GameManager.instance.gridLine, lineContainer).GetComponent<LineRenderer>();
            renderer.SetPosition(0, new Vector3(i * constants.GridCellSize, transform.position.y, gridSize * 2) + gridOrigin.position);
            renderer.SetPosition(1, new Vector3(i * constants.GridCellSize, transform.position.y, 0) + gridOrigin.position);

            renderer = Instantiate(GameManager.instance.gridLine, lineContainer).GetComponent<LineRenderer>();
            renderer.transform.parent = lineContainer;
            renderer.SetPosition(0, new Vector3(gridSize * 2, transform.position.y, i * constants.GridCellSize) + gridOrigin.position);
            renderer.SetPosition(1, new Vector3(0, transform.position.y, i * constants.GridCellSize) + gridOrigin.position);
        }
    }
}
