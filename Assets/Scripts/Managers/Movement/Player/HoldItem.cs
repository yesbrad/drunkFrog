using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldItem : MonoBehaviour
{
	private void Awake()
	{
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
		Gizmos.DrawWireCube(new Vector3(0.5f, 0.5f, 0.5f), Vector3.one);
	}
}
