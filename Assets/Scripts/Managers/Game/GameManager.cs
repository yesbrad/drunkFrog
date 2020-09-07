using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Houses
{
    Alpha,
    Beta,
    Charile,
    Daddy,
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public HouseManager[] houseManagers;

    [Header("BIBLE")]
    public DesignBible designBible;
    public DesignBible DesignBible
    {
        get
        {
            return designBible;
        }
    }

    [Header("DEBUG")]
    [SerializeField] int debugPlayerAmount;

    [Header("Static Prefabs")]
    public GameObject gridLine;
    public GameObject playerManagerPrefab;
    public GameObject AIManagerPrefab;
    public GameObject basicGroup;

    private List<PlayerManager> players = new List<PlayerManager>();
    private List<AIManager> ai = new List<AIManager>();

    public ItemData[] items;

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
        houseManagers[index].Init(player);
        player.Init(houseManagers[index]);
        players.Add(player);
    }

    /// <summary>
    /// Spawn AI into your selection of House
    /// </summary>
    /// <param name="manager">House to spawn in</param>
    public void SpawnAI(HouseManager manager)
    {
        manager.Spawner.SpawnPencil();
    }

    public void SpawnAI(Houses house)
    {
        SpawnAI(houseManagers[(int)house]);
    }

    private void Validate()
    {
        if (houseManagers.Length <= 0) Debug.LogError("Game Manager Missing HouseManagers");
        if (designBible == null) Debug.LogError("Game Manager Missing DESIGN BIBLE");
    }

    /// <summary>
    /// Calculate a modified percentage based off raw pp
    /// </summary>
    /// <param name="rawPP">Raw PP</param>
    public string CalculatePP (int rawPP)
    {
        return $"{Mathf.Floor(((float)rawPP / (float)designBible.ppScale) * 100)}%";
    }
}
