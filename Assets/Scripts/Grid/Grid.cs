using System.Collections.Generic;
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

    public void ResetSlot ()
    {
        gridState = GridSlotState.Open;
        item.Reset();
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

    public Grid (int height, int width, Transform origin)
    {
        this.height = height;
        this.width = width;
        this.cellSize = constants.GridCellSize;
        this.origin = origin;
        initilized = true;

        gridArray = new GridSlot[width * height];

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

    public Vector3 GetWorldPositionFromWorld (Vector3 position)
    {
        Vector2Int gr = GetGridPositionFromWorld(position);
        return GetWorldPositionFromGrid(gr.x, gr.y);
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
                    return GetValue(x, y).item;
                }
            }
        }

        return null;
    }

    public Vector3 GetRandomPosition ()
    {
        return GetWorldPositionFromGrid(UnityEngine.Random.Range(2, width - 3), UnityEngine.Random.Range(2, height - 3));
    }


    public int GetGridOneDIndex(int x, int y)
    {
        return y * width + x;
    }

    public void SetValue(int x, int y, Item value, GridSlotState gridSlotState = GridSlotState.Occupied, int size = 1, Transform rotateTransform = null)
    {
        if (gridArray == null)
        {
            Debug.Log("WE REALLT BROKEN");
            return;
        }

		if (CanPlaceItemWithSize(x, y, size, rotateTransform))
		{
			Vector2Int[] gridSpace = GetGridSpace(x, y, size, rotateTransform);

			for (int i = 0; i < gridSpace.Length; i++)
			{
				gridArray[GetGridOneDIndex(gridSpace[i].x, gridSpace[i].y)].item = value;
				gridArray[GetGridOneDIndex(gridSpace[i].x, gridSpace[i].y)].item.isPlaced = true;
				gridArray[GetGridOneDIndex(gridSpace[i].x, gridSpace[i].y)].gridState = gridSlotState;
			}
		}
		else
		{
			Debug.LogWarning("Value out of bounds");
		}       
    }

    /// <summary>
    /// Removes Item off the grid, Returns the Removed item
    /// </summary>
    public string DeleteValue(int x, int y)
    {
        if (gridArray == null)
        {
            Debug.Log("WE REALLT BROKEN");
            return null;
        }

        if (IsInBounds(x, y))
        {

            if(gridArray[GetGridOneDIndex(x, y)].gridState == GridSlotState.Occupied)
            {
                string cacheItemID = gridArray[GetGridOneDIndex(x, y)].item.id;

                for (int i = 0; i < gridArray.Length; i++)
                {
                    if (gridArray[GetGridOneDIndex(x, y)].item.UUID == gridArray[i].item.UUID)
                    {
                        gridArray[i].item.OnPickup();
                        gridArray[i].ResetSlot();
                    }
                }

                return cacheItemID;
            }
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

    public bool IsInBorderBounds(int x, int y)
    {
        return (x >= 0 && y >= 0 && x < width && y < height);
    }

    public bool CanPlaceItemWithSize(int x, int y, int size, Transform direction)
    {
        if (IsInBounds(x, y) == false)
            return false;

        Vector2Int[] gridSpace = GetGridSpace(x, y, size, direction);

        for (int i = 0; i < gridSpace.Length; i++)
        {
            if (IsInBounds(gridSpace[i].x, gridSpace[i].y) == false)
                return false;

            if (gridArray[GetGridOneDIndex(gridSpace[i].x, gridSpace[i].y)].gridState == GridSlotState.Occupied)
				return false;
        }

        return true;
    }

    public Vector2Int[] GetGridSpace(int x, int y, int size, Transform direction)
    {
        // No need to check for larger objects
        if(size == 1)
        {
            return new Vector2Int[] { new Vector2Int(x, y) };
        }

        List<Vector2Int> newSpace = new List<Vector2Int>();


		if(direction == null || PencilPartyUtils.RoundAnglesToNearest90(direction).y == 0)
		{
			for (int xx = 0; xx < size; xx++)
			{
				for (int yy = 0; yy < size; yy++)
				{
					newSpace.Add(new Vector2Int(x + xx, y + yy));
				}
			}
		}
		else if(PencilPartyUtils.RoundAnglesToNearest90(direction).y == 90)
		{
			for (int xx = 0; xx < size; xx++)
			{
				for (int yy = 0; yy < size; yy++)
				{
					newSpace.Add(new Vector2Int(x + xx, y - yy - 1));
				}
			}
		}
		else if(PencilPartyUtils.RoundAnglesToNearest90(direction).y == 180)
		{
			for (int xx = 0; xx < size; xx++)
			{
				for (int yy = 0; yy < size; yy++)
				{
					newSpace.Add(new Vector2Int(x + -xx - 1, y + -yy - 1));
				}
			}
		}
		else 
		{
			for (int xx = 0; xx < size; xx++)
			{
				for (int yy = 0; yy < size; yy++)
				{
					newSpace.Add(new Vector2Int(x + -xx - 1, y + yy));
				}
			}
		}

        return newSpace.ToArray();
    }

    public GridSlot GetValue(int x, int y)
    {
        if(gridArray == null)
        {
            Debug.Log("WE REALLT BROKEN" + gridArray);
            return null;
        }

        if (IsInBounds(x, y))
        {
            return gridArray[GetGridOneDIndex(x, y)];
        }

        Debug.LogWarning("Value out of bounds");
        return null;
    }
}
