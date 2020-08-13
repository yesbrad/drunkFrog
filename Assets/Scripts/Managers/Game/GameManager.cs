using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public HouseManager[] houseManagers;

    [Header("DEBUG")]
    [SerializeField] int debugPlayerAmount;

    [Header("Static Prefabs")]
    public GameObject gridLine;
    public GameObject playerManagerPrefab;

    private List<PlayerManager> players = new List<PlayerManager>();

    void Start()
    {
        Application.targetFrameRate = 60;
        instance = this;
        Validate();

        for (int i = 0; i < debugPlayerAmount; i++)
        {
            SpawnPlayer(i);
        }
    }

    public void SpawnPlayer (int index)
    {
        PlayerManager player = Instantiate(playerManagerPrefab).GetComponent<PlayerManager>();
        houseManagers[index].Init();
        player.Init(houseManagers[index]);
        players.Add(player);
    }

    private void Validate()
    {
        if (houseManagers.Length <= 0) Debug.LogError("Game Manager Missing HouseManagers");
    }
}
