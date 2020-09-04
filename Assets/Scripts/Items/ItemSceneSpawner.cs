using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class ItemSceneSpawner : MonoBehaviour
{
	public ItemData itemData;
	public bool spawnOnStart = true;
	private void Awake()
	{
		if(spawnOnStart)
			PlaceItem();
	}
	public void PlaceItem()
	{
		HouseManager[] managers = FindObjectsOfType<HouseManager>();

		GridController grid = null;

		foreach (HouseManager manager in managers)
		{
			if(grid == null)
				grid = manager.GetGrid(transform.position);
		}

		if (grid == null)
		{
			Debug.Log("No Grid Found");
			return;
		}

		Item item = ItemFactory.CreateItem(itemData, null);
		grid.PlaceItem(transform.position, item, null);
	}
	private void OnDrawGizmos()
	{
		if (itemData)
		{
			Gizmos.color = itemData.debugColor;
			Gizmos.DrawWireCube(transform.position + (Vector3.one * (itemData.size * constants.GridCellSize) / 2), Vector3.one * (itemData.size * constants.GridCellSize));
			Gizmos.DrawCube(transform.position + (Vector3.one * (itemData.size * constants.GridCellSize) / 2), Vector3.one * 0.3f);
		}
	}
}
