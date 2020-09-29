using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldItem : MonoBehaviour
{
	internal bool showOnAwake;

	private void Start()
	{
		if(!showOnAwake)
			HideItem();
	}

	public void ShowItem()
	{
		gameObject.SetActive(true);
	}

	public void HideItem()
	{
		gameObject.SetActive(false);
	}

	private void OnDrawGizmos()
	{
		Gizmos.matrix = transform.localToWorldMatrix;
		Gizmos.DrawWireCube(Vector3.up / 2, Vector3.one);
	}
}
