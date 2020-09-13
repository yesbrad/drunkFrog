using System.Collections.Generic;
using UnityEngine;

public class Pool
{
	public int baseAmount = 30;
	public GameObject poolPrefab;

	public Queue<GameObject> pool;

	public Pool(int baseAmount, GameObject poolPrefab, Transform parent)
	{
		this.baseAmount = baseAmount;
		this.poolPrefab = poolPrefab;

		pool = new Queue<GameObject>();

		for (int i = 0; i < baseAmount; i++)
		{
			GameObject newPool = GameObject.Instantiate(poolPrefab);
			newPool.transform.parent = parent;
			newPool.SetActive(false);
			pool.Enqueue(newPool);
		}
	}

	public GameObject GetFromPool(Vector3 position, Quaternion rotation)
	{
		GameObject newPool = pool.Dequeue();
		newPool.SetActive(true);
		newPool.transform.position = position;
		newPool.transform.rotation = rotation;
		return newPool;
	}

	public void AddBackToPool (GameObject t)
	{
		t.SetActive(false);
		pool.Enqueue(t);
	}

	public bool HasPool ()
	{
		return pool != null && pool.Count > 0;
	}
}