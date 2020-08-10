using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseManager : MonoBehaviour
{
    public const float c_gridCellSize = 2f;

    public GridController[] controllers;

    public GridController currentController;

    public void PlaceItem (Vector3 position, Item item)
    {
        currentController.grid.PlaceItemFromPosition(position, item);
    }
}
