using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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

    [Header("End Game")]
    [SerializeField]
    private Camera endCamera;

    [SerializeField]
    private Transform winningPosition;

    [Header("DEBUG")]
    [SerializeField] bool isDebug;
    [Range(1,4)]
    [SerializeField] int debugPlayerAmount;

    [SerializeField]
    public Transform storeSpawn;

    [Header("Static Prefabs")]
    public GameObject gridLine;
    public GameObject AIManagerPrefab;
    public GameObject basicGroup;

    internal List<PlayerInput> players = new List<PlayerInput>();

    public GameState State { get; private set; }

    public static event Action<GameState> OnUpdateState;

    private void Awake()
    {
        instance = this;
        //houseManagers = GameObject.FindObjectsOfType<HouseManager>();
    }

    void Start()
    {
        Application.targetFrameRate = 40;
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

        TimeManager.instance.StartDay();

        SetGameState(GameState.Game);
    }

    public void StartDebugGame ()
    {
        GameUI.instance.RefreshUI(PanelIDs.Game);

        for (int i = 0; i < debugPlayerAmount; i++)
        {
            SpawnPlayer(i);
        }

        TimeManager.instance.StartDay();

        SetGameState(GameState.Game);
    }

    public void ResetGame ()
    {
        SceneManager.LoadScene(0);
        //endCamera.gameObject.SetActive(false);
        //Destroy(winningPosition.GetChild(0).gameObject);
        //GameUI.instance.RefreshUI(PanelIDs.SOR);
    }

    public void EndGame ()
    {
        PlayerManager winningPlayer = players[0].GetComponent<PlayerManager>();

        for (int x = 0; x < players.Count; x++)
        {
            if (players[x].GetComponent<PlayerManager>().InitialHouse.CalculatePPFloat() > winningPlayer.InitialHouse.CalculatePPFloat())
            {
                winningPlayer = players[x].GetComponent<PlayerManager>();
            }
        }

        winningPlayer.Pawn.transform.parent = winningPosition;
        winningPlayer.Pawn.transform.localPosition = Vector3.zero;
        winningPlayer.Pawn.transform.localRotation = Quaternion.Euler(Vector3.zero);

        for (int i = 0; i < players.Count; i++)
        {
            Destroy(players[i].gameObject);
        }

        players.Clear();

        GameUI.instance.RefreshUI(PanelIDs.EOR);

        endCamera.gameObject.SetActive(true);

        SetGameState(GameState.Menu);
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


}
