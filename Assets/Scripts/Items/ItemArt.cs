using System.Collections.Generic;
using UnityEngine;

public class ItemArt : MonoBehaviour
{
	private MeshRenderer[] artPieces;

	private bool isSelected;

	private List<float> selections = new List<float>();

	private void Awake()
	{
		artPieces = gameObject.GetComponentsInChildren<MeshRenderer>();

		for (int i = 0; i < artPieces.Length; i++)
		{
			selections.Add(0);
		}
	}

	public void SetSelection (bool isSelected)
	{
		this.isSelected = isSelected;
	}

	private void Update()
	{		
		for (int i = 0; i < artPieces.Length; i++)
		{
			selections[i] = Mathf.Lerp(selections[i], isSelected ? 1 : 0, Time.deltaTime * 10);

			for (int m = 0; m < artPieces[i].materials.Length; m++)
			{
				artPieces[i].materials[m].SetFloat("_Select", selections[i]);
			}
		}
	}
}