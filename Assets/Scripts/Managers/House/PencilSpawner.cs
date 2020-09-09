using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PencilSpawner : MonoBehaviour, IStateListener
{
	[SerializeField]
	private Transform pencilSpawn;

	[SerializeField]
	private int groupSizeMin = 1;

	[SerializeField]
	private int groupSizeMax = 5;

	[SerializeField]
	private int groupRateMin = 1;

	[SerializeField]
	private int groupRateMax = 10;

	[SerializeField]
	private int partyLimit = 200;


	private float currentRate;
	public int amountSpawned;

	private HouseManager houseManager;

	public bool Initilized { get; private set; }

	private void Awake()
	{
		houseManager = GetComponent<HouseManager>();
		currentRate = CalculateRefreshTime();
		GameManager.OnUpdateState += (state) => OnGameStateUpdate(state);
	}

	private void Update()
	{
		currentRate -= Time.deltaTime;
	
		if(currentRate < 0)
		{
			for (int i = 0; i < CalculateGroupAmount(); i++)
			{
				SpawnPencil();
			}

			currentRate = CalculateRefreshTime();
		}
	}

	private float CalculateRefreshTime ()
	{
		return Random.Range(groupRateMin, groupRateMax);

	}

	private int CalculateGroupAmount()
	{
		return Random.Range(groupSizeMin, groupSizeMax);
	}

	public void SpawnPencil()
	{
		if (!Initilized || amountSpawned > partyLimit)
			return;

		AIManager newAI = Instantiate(GameManager.instance.AIManagerPrefab, pencilSpawn.position, Quaternion.identity).GetComponent<AIManager>();
		newAI.transform.parent = transform.transform;
		newAI.Init(houseManager);
		amountSpawned++;
	}

	public void OnGameStateUpdate(GameState gameState)
	{
		Initilized = gameState == GameState.Game;
	}
}
