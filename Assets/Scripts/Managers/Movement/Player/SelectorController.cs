using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorController : MonoBehaviour
{
	[SerializeField]
	private LineRenderer lineRenderer;

	public void UpdateLine (ItemSize size)
	{
		lineRenderer.SetPosition(1, new Vector3(0, 0, size.y * constants.GridCellSize));
		lineRenderer.SetPosition(2, new Vector3(size.x * constants.GridCellSize, 0, size.y * constants.GridCellSize));
		lineRenderer.SetPosition(3, new Vector3(size.x * constants.GridCellSize, 0, 0));
	}
}
