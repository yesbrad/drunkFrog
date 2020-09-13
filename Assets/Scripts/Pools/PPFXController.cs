using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PPFXController : MonoBehaviour
{
	public enum PPState
	{
		Plus,
		Minus,
	}

	public static PPFXController instance;

	[Header("Party Point FX")]
	[SerializeField]
	private GameObject plusFXPrefab;

	[SerializeField]
	private GameObject minusFXPrefab;

	[SerializeField]
	[Range(0, 150)]
	private int poolAmount = 30;

	private Pool plusPool;
	private Pool minusPool;

	private void Awake()
	{
		instance = this;
		plusPool = new Pool(poolAmount, plusFXPrefab, transform);
		minusPool = new Pool(poolAmount, minusFXPrefab, transform);
	}

	public void Play (PPState state, Vector3 position)
	{
		Pool currentPool = state == PPState.Minus ? minusPool : plusPool;

		if (currentPool.HasPool())
		{
			GameObject spawn = currentPool.GetFromPool(position, Quaternion.identity);
			spawn.GetComponent<PPFXParticle>()?.Play(currentPool);
		}
	}
}
