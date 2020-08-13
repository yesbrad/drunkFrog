using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public HouseManager HouseManager { get; private set; }
    public GridController CurrentGrid { get; private set; }

    public Pawn Pawn { get; private set; }

    public virtual void Awake()
    {
        Pawn = GetComponentInChildren<Pawn>();
    }

    public virtual void Init(HouseManager initialHouse)
    {
        SetHouse(initialHouse);
    }

    public void SetHouse(HouseManager house)
    {
        if(HouseManager && HouseManager.guests.Contains(this))
        {
            HouseManager.guests.Remove(this);
        }
        
        HouseManager = house;
        HouseManager.guests.Add(this);
    }

    public void SetGrid(GridController grid)
    {
        CurrentGrid = grid;
    }

    public virtual void Update()
    {
        if (!HouseManager) return;

        CheckForNewGrid();
    }

    private void CheckForNewGrid()
    {
        float minDis = Mathf.Infinity;

        // Check for current floor
        for (int i = 0; i < HouseManager.gridControllers.Length; i++)
        {
            float dis = Vector3.Distance(new Vector3(Pawn.Position.x, HouseManager.gridControllers[i].gridOrigin.position.y, Pawn.Position.z), Pawn.Position + Vector3.down);
            if (dis < minDis)
            {
                CurrentGrid = HouseManager.gridControllers[i];
                minDis = dis;
            }
        }
    }

    public virtual void PlaceOrUseItem(Vector3 position, Item item)
    {
        if (HasActiveGrid())
        {
            CurrentGrid.PlaceOrUseItem(position, item, this);
            Debug.Log("Plaving item: " + item.name);
        }
    }

    public Vector3 GetGridPosition(Vector3 lookPosition)
    {
        return CurrentGrid.grid.GetWorldGridCenterPositionFromWorld(lookPosition);
    }

    public void LayerCullingShow(Camera cam, int layerMask)
    {
        cam.cullingMask |= layerMask;
    }

    public void LayerCullingHide(Camera cam, int layerMask)
    {
        cam.cullingMask &= ~layerMask;
    }

    public bool HasActiveGrid ()
    {
        return CurrentGrid != null;
    }
}
