using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class GridBlocker : MonoBehaviour
{
	public ItemSize size;

	[SerializeField]
	internal bool showGizmos;
	
	private void Start()
	{
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
			Debug.Log("No Grid Found", gameObject);
			return;
		}

		grid.OccupieSpace(transform.position + GetPlacePosition(), size, transform);
	}

	private void OnDrawGizmos()
	{
		if (showGizmos)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawSphere(transform.position + GetPlacePosition(), 0.3f);
			Gizmos.DrawLine(transform.position + GetPlacePosition(), (transform.position + GetPlacePosition()) - transform.forward);
			Gizmos.matrix = transform.localToWorldMatrix;
			Gizmos.DrawLine(Vector3.zero, Vector3.right * (size.x * constants.GridCellSize));
			Gizmos.DrawLine(Vector3.zero, Vector3.forward * (size.y * constants.GridCellSize));

			Gizmos.DrawLine(Vector3.right * (size.x * constants.GridCellSize), (Vector3.right * (size.x * constants.GridCellSize)) +
				(Vector3.forward * (size.y * constants.GridCellSize)));
			Gizmos.DrawLine(Vector3.forward * (size.y * constants.GridCellSize), (Vector3.right * (size.x * constants.GridCellSize)) +
				(Vector3.forward * (size.y * constants.GridCellSize)));

			Color color = Color.red;
			color.a = 0.5f;
			Gizmos.color = color;

			for (int i = 0; i < size.x; i++)
			{
				for (int y = 0; y < size.y; y++)
				{
					Gizmos.DrawCube((Vector3.one) + new Vector3(i * constants.GridCellSize, -1, y * constants.GridCellSize), new Vector3(1,0,1) * constants.GridCellSize);
				}
			}
			
		}
	}

	private Vector3 GetPlacePosition()
	{
		return transform.TransformVector(Vector3.zero + new Vector3(1, 0 , 1));
	}
}
