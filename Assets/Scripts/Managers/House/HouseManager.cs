﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseManager : MonoBehaviour
{
    [System.Serializable]
    public struct FloorSettings
    {
        public GameObject floorPrefab;
        public Transform floorDetectionSocket;
        public LayerMask cameraLayer;
    }

    [SerializeField]
    internal FloorSettings[] floorSettings;
    
    [SerializeField]
    private Transform houseCenter;

    [SerializeField]
    private Transform spawnPosition;

    [SerializeField]
    private int baseCash = 10000;

    [Header("Debug")]

    public List<CharacterManager> guests = new List<CharacterManager>();

    public PlayerManager houseOwner;

    public int BaseCash { get { return baseCash; } }

    public PencilSpawner Spawner { get; private set; }
    public HouseInventory HouseInventory { get; private set; }
    public Vector3 SpawnPosition { get { return spawnPosition.position; } }

    private int PP;
    
    private GridController[] gridControllers;

    private void Awake()
    {
        gridControllers = GetComponentsInChildren<GridController>();
        Spawner = GetComponent<PencilSpawner>();
        HouseInventory = GetComponent<HouseInventory>();
    }

    public void Init (PlayerManager owner)
    {
        houseOwner = owner;
        PP = 1;
        RefreshUI();
    }

    public void AddPP (int amount, Vector3 position)
    {
        PPFXController.instance.Play(PPFXController.PPState.Plus, position);
        AddPP(amount);
    }

    public void AddPP(int amount)
    {
        if (amount == 0)
            return;

        PP += Mathf.Abs(amount);
        RefreshUI();
    }

    public void RemovePP(int amount, Vector3 position)
    {
        PPFXController.instance.Play(PPFXController.PPState.Minus, position);
        RemovePP(amount);
    }

    public void RemovePP(int amount)
    {
        if (amount == 0)
            return;

        PP -= Mathf.Abs(amount);
        RefreshUI();
    }

    public void RefreshUI ()
    {   
        if(houseOwner != null)
            houseOwner.PlayerUI.SetPP(GameManager.instance.CalculatePP(PP));
    }

    public Vector3 GetCenterPoint()
    {
        if(houseCenter != null)
        {
            return houseCenter.position;
        }

        Debug.LogError("House is missing center point varible!", gameObject);
        return Vector3.zero;
    }

    public GridController GetGrid(Vector3 position)
    {
        GridController controller = null;

        for (int i = 0; i < gridControllers.Length; i++)
        {
            if (gridControllers[i].IsInBorderBounds(position))
            {
                float distaneBetweenFloorHeightAndPawn = Vector3.Distance(new Vector3(position.x, gridControllers[i].transform.position.y, position.z), position);

                if (distaneBetweenFloorHeightAndPawn < 0.5f)
                {
                    controller = gridControllers[i];
                    return controller;
                }
            }
        }

        return controller;
    }
}