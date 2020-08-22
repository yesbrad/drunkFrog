using UnityEngine;

public enum GridSlotState
{
    Open,
    Occupied,
    Blocked,
}

[System.Serializable]
public class GridSlot
{
    public Item item;
    public GridSlotState gridState;

    public GridSlot (Item newItem, GridSlotState occ)
    {
        item = newItem;
        gridState = occ;
    }
}

[System.Serializable]
public class Grid
{
    public int height;
    public int width;
    public float cellSize;
    public Transform origin;
    public bool initilized;
    
    [SerializeField] GridSlot[] gridArray;

    public GridSlot[] GridArray { get { return gridArray; } }

    public Grid (int height, int width, float cellSize, Transform origin)
    {
        this.height = height;
        this.width = width;
        this.cellSize = cellSize;
        this.origin = origin;
        initilized = true;

        gridArray = new GridSlot[width * height];
        Debug.Log($"Grid Length: { gridArray.Length}");

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                gridArray[y*width+x] = new GridSlot(null, GridSlotState.Open);
            }
        }
    }

    public Vector3 GetWorldPositionFromGrid (int x, int y)
    {
        return new Vector3(x, 0, y) * cellSize + origin.position;
    }

    public Vector3 GetWorldGridCenterPositionFromWorld(Vector3 position)
    {
        Vector2Int gr = GetGridPositionFromWorld(position);
        Vector3 worldPos = GetWorldPositionFromGrid(gr.x, gr.y);
        return new Vector3(worldPos.x + (constants.GridCellSize / 2), origin.position.y , worldPos.z + (constants.GridCellSize / 2));
    }

    public Vector2Int GetGridPositionFromWorld (Vector3 _worldPosition)
    {
        return new Vector2Int(Mathf.FloorToInt((_worldPosition - origin.position ).x / cellSize), Mathf.FloorToInt((_worldPosition - origin.position).z / cellSize));
    }

    public Item GetRandomItem ()
    {
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                if(GetValue(x,y) != null)
                {
                    return GetValue(x, y);
                }
            }
        }

        return null;
    }

    public Vector3 GetRandomPosition ()
    {
        return GetWorldPositionFromGrid(Random.Range(2, width - 3), Random.Range(2, height - 3));
    }


    public int GetGridOneDIndex(int x, int y)
    {
        return y * width + x;
    }
    public void SetValue(int x, int y, Item value, GridSlotState gridSlotState = GridSlotState.Occupied)
    {
        if (gridArray == null)
        {
            Debug.Log("WE REALLT BROKEN");
            return;
        }

        if (IsInBounds(x,y))
        {
            gridArray[GetGridOneDIndex(x, y)].item = value;
            gridArray[GetGridOneDIndex(x, y)].item.isPlaced = true;
            gridArray[GetGridOneDIndex(x, y)].gridState = gridSlotState;
        }
        else
        {
            Debug.LogWarning("Value out of bounds");
        }
    }

    /// <summary>
    /// Removes Item off the grid, Returns the Removed item
    /// </summary>
    public Item DeleteValue(int x, int y)
    {
        if (gridArray == null)
        {
            Debug.Log("WE REALLT BROKEN");
            return null;
        }

        if (IsInBounds(x, y))
        {
            gridArray[GetGridOneDIndex(x, y)].item.OnPickup();
            gridArray[GetGridOneDIndex(x, y)].item.isPlaced = false;
            gridArray[GetGridOneDIndex(x, y)].gridState = GridSlotState.Open;
            return gridArray[GetGridOneDIndex(x, y)].item;
        }
        else
        {
            Debug.LogWarning("Value out of bounds");
        }

        return null;
    }

    public bool IsInBounds (int x, int y)
    {
        return (x >= 0 && y >= 0 && x < width && y < height) && gridArray[GetGridOneDIndex(x, y)].gridState != GridSlotState.Blocked;
    }

    public Item GetValue(int x, int y)
    {
        if(gridArray == null)
        {
            Debug.Log("WE REALLT BROKEN" + gridArray);
            return null;
        }

        if (IsInBounds(x, y))
        {
            //Debug.Log($"Is GridSlotNull: {gridArray[x, y] == null}");
            //Debug.Log($"Occipied: {gridArray[x, y].occupied}");
            //Debug.Log($"Item: {gridArray[x, y].item}");

            if (gridArray[GetGridOneDIndex(x, y)].gridState == GridSlotState.Occupied && gridArray[GetGridOneDIndex(x, y)].item != null)
            {
                //Debug.Log($"GET VAL: {x} : {y}");
                // Debug.Log($"Grid Array: {gridArray[x,y
                return gridArray[GetGridOneDIndex(x, y)].item;
            } 
        }

        Debug.LogWarning("Value out of bounds");
        return null;
    }
}
