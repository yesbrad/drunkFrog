using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PPFXController : MonoBehaviour
{
	public enum PPState
	{
		Plus,
		Minus,
		MoneyPlus,
		MoneyMinus,
		outOfOrder,
	}

	public static PPFXController instance;

	[Header("Money FX")]
	[SerializeField]
	private GameObject moneyPlusFXPrefab;

	[SerializeField]
	private GameObject moneyMinusFXPrefab;

	[Header("Party Point FX")]
	[SerializeField]
	private GameObject plusFXPrefab;

	[SerializeField]
	private GameObject minusFXPrefab;

	[Header("Store FX")]

	[SerializeField]
	private GameObject outOfOrderPrefab;

	[SerializeField]
	[Range(0, 150)]
	private int poolAmount = 30;

	private Pool plusPool;
	private Pool minusPool;

	private Pool moneyMinusPool;
	private Pool moneyPlusPool;
	private Pool outOfOrderPool;

	private void Awake()
	{
		instance = this;
		plusPool = new Pool(poolAmount, plusFXPrefab, transform);
		minusPool = new Pool(poolAmount, minusFXPrefab, transform);
		moneyPlusPool = new Pool(poolAmount, moneyPlusFXPrefab, transform);
		moneyMinusPool = new Pool(poolAmount, moneyMinusFXPrefab, transform);
		outOfOrderPool = new Pool(poolAmount, outOfOrderPrefab, transform);
	}

	public void Play (PPState state, Vector3 position)
	{
		Pool currentPool = GetPool(state);

		if (currentPool.HasPool())
		{
			GameObject spawn = currentPool.GetFromPool(position, Quaternion.identity);
			spawn.GetComponent<PPFXParticle>()?.Play(currentPool);
		}
	}

	private Pool GetPool(PPState state)
	{
		switch (state)
		{
			case PPState.Minus:
				return minusPool;
			case PPState.Plus:
				return plusPool;
			case PPState.MoneyMinus:
				return moneyMinusPool;
			case PPState.MoneyPlus:
				return moneyPlusPool;
			case PPState.outOfOrder:
			return outOfOrderPool;
		}

		return moneyPlusPool;
	}
}
