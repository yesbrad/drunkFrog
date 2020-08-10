using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public int gridSize;
    public Transform gridOrigin;
    public Grid grid;

    void Start()
    {
        grid = new Grid(gridSize, gridSize, HouseManager.c_gridCellSize, gridOrigin.position);
    }
}
