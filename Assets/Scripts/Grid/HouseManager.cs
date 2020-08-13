using System.Collections;
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

    public GridController[] gridControllers;
    public FloorSettings[] floorSettings;

    public List<CharacterManager> guests = new List<CharacterManager>();

    public PlayerManager houseOwner;

    public int HP;

    public void Init (PlayerManager owner)
    {
        houseOwner = owner;
    }

    public void AddHP (int amount)
    {
        HP += amount;
    }
}
