using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class ItemSceneSpawner : MonoBehaviour
{
	public ItemData itemData;
	public bool spawnOnStart = true;
	public bool showGizmos = true;
	public bool Spawned { get; private set; }
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

		Spawned = true;
	}
	private void OnDrawGizmos()
	{
		if (itemData != null && showGizmos)
		{
			Gizmos.color = itemData.debugColor;
			Gizmos.DrawLine(transform.position, transform.position + Vector3.right * (itemData.size.x * constants.GridCellSize));
			Gizmos.DrawLine(transform.position, transform.position + Vector3.forward * (itemData.size.y * constants.GridCellSize));

			Gizmos.DrawLine(transform.position + Vector3.right * (itemData.size.x * constants.GridCellSize), (Vector3.right * (itemData.size.x * constants.GridCellSize)) +
				(Vector3.forward * (itemData.size.y * constants.GridCellSize)) + transform.position);
			Gizmos.DrawLine(transform.position + Vector3.forward * (itemData.size.y * constants.GridCellSize), (Vector3.right * (itemData.size.x * constants.GridCellSize)) +
				(Vector3.forward * (itemData.size.y * constants.GridCellSize)) + transform.position);

			Gizmos.DrawSphere(transform.position, 0.3f);
		}
	}
}
