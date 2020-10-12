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
	
	private void Start()
	{
		if(spawnOnStart)
			PlaceItem();
	}

	public void PlaceItem()
	{
		HouseManager[] managers = FindObjectsOfType<HouseManager>();

		GridController grid = null;
		HouseManager startHouse = null;

		foreach (HouseManager manager in managers)
		{
			if(grid == null)
			{
				grid = manager.GetGrid(transform.position + GetPlacePosition());
				startHouse = manager;
			}
		}

		if (grid == null)
		{
			Debug.LogWarning("No Grid Found", gameObject);
			return;
		}

		Item item = ItemFactory.CreateItem(itemData, startHouse);
		grid.PlaceItem(transform.position + GetPlacePosition(), item, transform);

		Spawned = true;
	}
	private void OnDrawGizmos()
	{
		if (itemData != null && showGizmos)
		{
			Gizmos.color = itemData.debugColor;
			Gizmos.DrawSphere(transform.position + GetPlacePosition(), 0.3f);
			Gizmos.DrawLine(transform.position + GetPlacePosition(), (transform.position + GetPlacePosition()) - transform.forward);
			Gizmos.matrix = transform.localToWorldMatrix;
			Gizmos.DrawLine(Vector3.zero, Vector3.right * (itemData.size.x * constants.GridCellSize));
			Gizmos.DrawLine(Vector3.zero, Vector3.forward * (itemData.size.y * constants.GridCellSize));

			Gizmos.DrawLine(Vector3.right * (itemData.size.x * constants.GridCellSize), (Vector3.right * (itemData.size.x * constants.GridCellSize)) +
				(Vector3.forward * (itemData.size.y * constants.GridCellSize)));
			Gizmos.DrawLine(Vector3.forward * (itemData.size.y * constants.GridCellSize), (Vector3.right * (itemData.size.x * constants.GridCellSize)) +
				(Vector3.forward * (itemData.size.y * constants.GridCellSize)));

		}
	}

	private Vector3 GetPlacePosition()
	{
		return transform.TransformVector(Vector3.zero + new Vector3(1, 0 , 1));
	}
}
