
using System;
using UnityEngine;

public class Grid
{
    public int height;
    public int width;
    public float cellSize;
    public Vector3 origin;

    private int[,] gridArray;

    public Grid (int height, int width, float cellSize, Vector3 origin)
    {
        this.height = height;
        this.width = width;
        this.cellSize = cellSize;
        this.origin = origin;

        gridArray = new int[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                Debug.Log(y + " : " + x);
                Debug.DrawLine(GetWorldPositionFromGrid(x, y + 1), GetWorldPositionFromGrid(x, y), Color.red, 100f);
                Debug.DrawLine(GetWorldPositionFromGrid(x + 1, y), GetWorldPositionFromGrid(x, y), Color.red, 100f);
            }
        }

        Debug.DrawLine(GetWorldPositionFromGrid(width, height), GetWorldPositionFromGrid(width, 0), Color.red, 100f);
        Debug.DrawLine(GetWorldPositionFromGrid(width, height), GetWorldPositionFromGrid(0, height), Color.red, 100f);
    }

    public Vector3 GetWorldPositionFromGrid (int x, int y)
    {
        return new Vector3(x, 0, y) * cellSize + origin;
    }

    public Vector2 GetGridPositionFromWorld (Vector3 _worldPosition)
    {
        return new Vector2(Mathf.FloorToInt((_worldPosition - origin ).x / cellSize), Mathf.FloorToInt((_worldPosition - origin).z / cellSize));
    }

    public void SetValue(int x, int y, int value)
    {
        if(x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = value;
        } 
        else
        {
            Debug.LogWarning("Value out of bounds");
        }
    }

    public int GetValue(int x, int y, int value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        }

        Debug.LogWarning("Value out of bounds");
        return 0;
    }
}
