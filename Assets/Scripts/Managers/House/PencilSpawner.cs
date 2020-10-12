using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PencilSpawner : MonoBehaviour, IStateListener
{
	[SerializeField]
	private AISpawnerSettings spawnerSettings;

	private float currentRate;
	public int amountSpawned;

	private HouseManager houseManager;

	public bool Initilized { get; private set; }

	private void Awake()
	{
		houseManager = GetComponent<HouseManager>();
		currentRate = CalculateRefreshTime();
		GameManager.OnUpdateState += (state) => OnGameStateUpdate(state);
		TimeManager.onTimeChange += (time) => UpdateSpawn(time);
	}

	private void UpdateSpawn(float time)
	{
		currentRate -= Time.deltaTime;
	
		if(currentRate < 0)
		{
			if (Random.Range(0, 1) < time && time > 0.5f)
			{
				for (int i = 0; i < CalculateGroupAmount(); i++)
				{
					SpawnPencil();
				}
			}

			currentRate = CalculateRefreshTime();
		}
	}

	private float CalculateRefreshTime ()
	{
		return Random.Range(spawnerSettings.groupRateMin, spawnerSettings.groupRateMax);

	}

	private int CalculateGroupAmount()
	{
		return Random.Range(spawnerSettings.groupSizeMin, spawnerSettings.groupSizeMax);
	}

	public void SpawnPencil()
	{
		if (!Initilized || amountSpawned > spawnerSettings.partyLimit)
			return;

		AIManager newAI = Instantiate(GameManager.instance.AIManagerPrefab, transform.position, Quaternion.identity).GetComponent<AIManager>();
		newAI.transform.parent = transform.transform;
		newAI.Init(houseManager, GetNewClass());
		amountSpawned++;
	}

	public AIClass GetNewClass ()
	{
		return spawnerSettings.aiClasses[0];
	}

	public void OnGameStateUpdate(GameState gameState)
	{
		Initilized = gameState == GameState.Game;
	}
}
