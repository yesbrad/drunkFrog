using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    [SerializeField] bool isDebug;
    [Range(1,4)]
    [SerializeField] int debugPlayerAmount;

    [Header("Static Prefabs")]
    public GameObject gridLine;
    public GameObject AIManagerPrefab;
    public GameObject basicGroup;

    public ItemData[] items;

    public List<PlayerInput> players = new List<PlayerInput>();

    public GameState State { get; private set; }

    public static event Action<GameState> OnUpdateState;

    void Start()
    {
        Application.targetFrameRate = 40;
        instance = this;
        Validate();

        GameUI.instance.RefreshUI(PanelIDs.Join);

        SetGameState(GameState.Menu);

        if (isDebug)
        {
            StartDebugGame();
        }
    }

    public void StartGame()
    {
        GameUI.instance.RefreshUI(PanelIDs.Game);

        for (int i = 0; i < players.Count; i++)
        {
            players[i].GetComponent<PlayerManager>().Init(houseManagers[i]);
            houseManagers[i].Init(players[i].GetComponent<PlayerManager>());
        }

        SetGameState(GameState.Game);
    }

    public void StartDebugGame ()
    {
        GameUI.instance.RefreshUI(PanelIDs.Game);

        for (int i = 0; i < debugPlayerAmount; i++)
        {
            SpawnPlayer(i);
        }

        SetGameState(GameState.Game);
    }

    public void SetGameState (GameState state)
    {
        State = state;
        OnUpdateState?.Invoke(state);
    }

    public void SpawnPlayer (int index)
    {
        PlayerManager player = Instantiate(PlayerInputManager.instance.playerPrefab).GetComponent<PlayerManager>();
        houseManagers[index].Init(player);
        player.Init(houseManagers[index]);
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
