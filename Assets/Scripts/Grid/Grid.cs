using UnityEngine;

[System.Serializable]
public class GridSlot
{
    public Item item;
    public bool occupied;

    public GridSlot (Item newItem, bool occ)
    {
        item = newItem;
        occupied = occ;
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
                gridArray[y*width+x] = new GridSlot(null, false);
                //Debug.DrawLine(GetWorldPositionFromGrid(x, y + 1), GetWorldPositionFromGrid(x, y), Color.red, 100f);
                //Debug.DrawLine(GetWorldPositionFromGrid(x + 1, y), GetWorldPositionFromGrid(x, y), Color.red, 100f);
            }
        }

        //Debug.DrawLine(GetWorldPositionFromGrid(width, height), GetWorldPositionFromGrid(width, 0), Color.red, 100f);
        //Debug.DrawLine(GetWorldPositionFromGrid(width, height), GetWorldPositionFromGrid(0, height), Color.red, 100f);
    }

    //public void GetGr

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
    public void SetValue(int x, int y, Item value)
    {
        if (gridArray == null)
        {
            Debug.Log("WE REALLT BROKEN");
            return;
        }

        if (IsInBounds(x,y))
        {
            gridArray[GetGridOneDIndex(x, y)].item = value;
            gridArray[GetGridOneDIndex(x, y)].occupied = true;
        }
        else
        {
            Debug.LogWarning("Value out of bounds");
        }
    }

    public bool IsInBounds (int x, int y)
    {
        return (x >= 0 && y >= 0 && x < width && y < height);
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

            if (gridArray[GetGridOneDIndex(x, y)].occupied && gridArray[GetGridOneDIndex(x, y)].item != null)
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
