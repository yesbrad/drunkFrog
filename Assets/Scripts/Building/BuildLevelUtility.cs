using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildLevelUtility : MonoBehaviour
{
	public void UpdateBlockLayer()
	{
		BuildBlock[] block = gameObject.GetComponentsInChildren<BuildBlock>();

		for (int i = 0; i < block.Length; i++)
		{
			block[i].gameObject.layer = gameObject.layer;

			foreach (Transform t in block[i].gameObject.transform)
			{
				t.gameObject.layer = gameObject.layer;
			}
		}
	}
}
