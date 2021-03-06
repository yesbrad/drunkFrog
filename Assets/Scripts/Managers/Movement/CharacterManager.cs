﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public HouseManager CurrentHouse { get; private set; }
    public HouseManager InitialHouse { get; private set; }

    public GridController CurrentGrid { get; private set; }
    public InventoryManager InventoryManager { get; private set; }

	public Transform RotationContainer { get; private set; }

    [HideInInspector]
	public Transform playerDirectionContainer;

    public Pawn Pawn { get; private set; }

    public bool Initialized { get; private set; }

    public virtual void Awake()
    {
        Pawn = GetComponentInChildren<Pawn>();
        Pawn.Init(this);
        InventoryManager = GetComponent<InventoryManager>();
    }

    public virtual void Init(HouseManager initialHouse)
    {
        Initialized = true;
        InitialHouse = initialHouse;
        SetHouse(initialHouse);
    }

    public void SetHouse(HouseManager house)
    {
        if(CurrentHouse && CurrentHouse.guests.Contains(this))
        {
            CurrentHouse.guests.Remove(this);
        }
        
        CurrentHouse = house;
        CurrentHouse.guests.Add(this);
    }

    public void SetGrid(GridController grid)
    {
        CurrentGrid = grid;
    }

	public void SetRotationContainer (Transform rotationTransform){
		RotationContainer = rotationTransform;
	}

    public virtual void Update()
    {
        if (!CurrentHouse) return;

        CheckForNewGrid();
    }

    private void CheckForNewGrid()
    {
        CurrentGrid = CurrentHouse.GetGrid(Pawn.Position);
    }

    public void PlaceOrPickupCurrentItem(Vector3 position)
    {
        if(InventoryManager != null && HasActiveGrid())
        {
            if (InventoryManager.HasItem())
            {
                if (CurrentGrid.PlaceItem(position, InventoryManager.CurrentItem, this.RotationContainer))
                {
                    InventoryManager.ConsumeItem();
                    return;
                }
            }
            else
            {
                Item possiblePickup = CurrentGrid.DeleteItem(position, CurrentHouse);
            
                if(possiblePickup != null)
                {
                    InventoryManager.GiveItem(possiblePickup);
                }
            }
        }
    }

    public Vector3 GetGridCenterPosition(Vector3 lookPosition)
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
