using System.Collections.Generic;
using UnityEngine;

public enum GridSlotState
{
    Open,
    Occupied,
    Blocked,
    Static,
}

[System.Serializable]
public struct GridPosition
{
    public int x;
    public int y;

    public GridPosition (int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public Vector2Int ToVector2Int()
    {
        return new Vector2Int(x, y);
    }
}

[System.Serializable]
public class GridSlot
{
    public Item item;
    public GridSlotState gridState;
    public GridPosition position;

    public GridSlot (Item newItem, GridSlotState occ, GridPosition position)
    {
        item = newItem;
        gridState = occ;
        this.position = position;
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
                gridArray[y*width+x] = new GridSlot(null, GridSlotState.Open, new GridPosition(x,y));
            }
        }
    }

    public Vector3 GetWorldPositionFromGrid(GridPosition position)
    {
        return GetWorldPositionFromGrid(position.x, position.y);
    }

    public Vector3 GetWorldPositionFromGrid (int x, int y)
    {
        return new Vector3(x, 0, y) * cellSize + origin.position;
    }

    public Vector3 GetWorldGridCenterPositionFromWorld(Vector3 position)
    {
        GridPosition gr = GetGridPositionFromWorld(position);
        Vector3 worldPos = GetWorldPositionFromGrid(gr.x, gr.y);
        return new Vector3(worldPos.x + (constants.GridCellSize / 2), origin.position.y , worldPos.z + (constants.GridCellSize / 2));
    }

    public Vector3 GetWorldGridCenterPositionFromGrid(GridPosition position)
    {
        return GetWorldGridCenterPositionFromGrid(position.x, position.y);
    }

    public Vector3 GetWorldGridCenterPositionFromGrid(int x, int y)
    {
        Vector3 worldPos = GetWorldPositionFromGrid(x, y);
        return new Vector3(worldPos.x + (constants.GridCellSize / 2), origin.position.y, worldPos.z + (constants.GridCellSize / 2));
    }

    public Vector3 GetWorldPositionFromWorld (Vector3 position)
    {
        GridPosition gr = GetGridPositionFromWorld(position);
        return GetWorldPositionFromGrid(gr.x, gr.y);
    }

    private GridPosition tempPos = new GridPosition(0, 0);

    public GridPosition GetGridPositionFromWorld (Vector3 position)
    {
        tempPos.x = GetPosX(position);
        tempPos.y = GetPosY(position);
        return tempPos;
    }

    private int GetPosX(Vector3 pos) => Mathf.FloorToInt((pos - origin.position).x / cellSize);
    private int GetPosY(Vector3 pos) => Mathf.FloorToInt((pos - origin.position).z / cellSize);

    public Vector3 GetOpenRandomPosition ()
    {
        GridSlot pos = GetRandomSlot();

        //Debug.Log("" + pos.position.x + " : " + pos.position.y);
        //Debug.Log("" + gridArray[34].position.x + " : " + gridArray[34].position.y);

        while (pos.gridState != GridSlotState.Open)
        {
            pos = GetRandomSlot();
        }


        return GetWorldGridCenterPositionFromWorld(GetWorldPositionFromGrid(pos.position));
    }

    public GridSlot GetRandomSlot()
    {
        int x = UnityEngine.Random.Range(1, width - 1);
        int y = UnityEngine.Random.Range(1, height - 1);
        return GetValue(x,y);
    }

    public int GetGridOneDIndex(int x, int y)
    {
        return y * width + x;
    }

    public void SetValue(int x, int y, Item value, ItemSize size, GridSlotState gridSlotState = GridSlotState.Occupied, Transform rotateTransform = null)
    {
        if (gridArray == null)
        {
            Debug.Log("WE REALLT BROKEN");
            return;
        }

		if (CanPlaceItemWithSize(x, y, size, rotateTransform))
		{
            GridPosition[] gridSpace = GetGridSpace(x, y, size, rotateTransform);

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

    public void SetBlocked(int x, int y, ItemSize size, Transform rotateTransform)
    {
        if (gridArray == null)
        {
            Debug.Log("WE REALLT BROKEN");
            return;
        }

        if (CanPlaceItemWithSize(x, y, size, rotateTransform))
        {
            GridPosition[] gridSpace = GetGridSpace(x, y, size, rotateTransform);

            for (int i = 0; i < gridSpace.Length; i++)
            {
                gridArray[GetGridOneDIndex(gridSpace[i].x, gridSpace[i].y)].gridState = GridSlotState.Blocked;
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
    public Item DeleteValue(int x, int y, HouseManager manager, bool hardDelete = false)
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
                Item oldItem = gridArray[GetGridOneDIndex(x, y)].item;
                ItemController itemController = oldItem.controller;
                itemController.OnPickup();
                Item possibleClonedItem = ItemFactory.CloneItem(oldItem.Data, itemController, manager, oldItem.extraData);

                for (int i = 0; i < gridArray.Length; i++)
                {
                    if (gridArray[GetGridOneDIndex(x, y)].item.UUID == gridArray[i].item.UUID)
                    {
                        gridArray[i].ResetSlot();
                    }
                }

                return possibleClonedItem;
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

    public bool CanPlaceItemWithSize(int x, int y, ItemSize size, Transform direction)
    {
        if (IsInBounds(x, y) == false)
            return false;

        GridPosition[] gridSpace = GetGridSpace(x, y, size, direction);

        for (int i = 0; i < gridSpace.Length; i++)
        {
            if (IsInBounds(gridSpace[i].x, gridSpace[i].y) == false)
                return false;

            if (gridArray[GetGridOneDIndex(gridSpace[i].x, gridSpace[i].y)].gridState == GridSlotState.Occupied)
				return false;

            if (gridArray[GetGridOneDIndex(gridSpace[i].x, gridSpace[i].y)].gridState == GridSlotState.Static)
                return false;
        }

        return true;
    }

    public GridPosition[] GetGridSpace(int x, int y, ItemSize size, Transform direction)
    {
        // No need to check for larger objects
        if(size.IsSingle())
        {
            return new GridPosition[] { new GridPosition(x, y) };
        }

        List<GridPosition> newSpace = new List<GridPosition>();

		if(direction == null || PencilPartyUtils.RoundAnglesToNearest90(direction).y == 0)
		{
			for (int xx = 0; xx < size.x; xx++)
			{
				for (int yy = 0; yy < size.y; yy++)
				{
					newSpace.Add(new GridPosition(x + xx, y + yy));
				}
			}
		}
		else if(PencilPartyUtils.RoundAnglesToNearest90(direction).y == 90)
		{
			for (int xx = 0; xx < size.y; xx++)
			{
				for (int yy = 0; yy < size.x; yy++)
				{
					newSpace.Add(new GridPosition(x + xx, y - yy));
				}
			}
		}
		else if(PencilPartyUtils.RoundAnglesToNearest90(direction).y == 180)
		{
			for (int xx = 0; xx < size.x; xx++)
			{
				for (int yy = 0; yy < size.y; yy++)
				{
					newSpace.Add(new GridPosition(x + -xx, y + -yy));
				}
			}
		}
		else 
		{
			for (int xx = 0; xx < size.y; xx++)
			{
				for (int yy = 0; yy < size.x; yy++)
				{
					newSpace.Add(new GridPosition(x + -xx, y + yy));
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

        if (IsInBorderBounds(x, y))
        {
            return gridArray[GetGridOneDIndex(x, y)];
        }

        Debug.LogError("Value out of bounds");
        return null;
    }
}
