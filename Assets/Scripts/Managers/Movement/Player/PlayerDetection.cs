using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
public class PlayerDetection : MonoBehaviour
{
	[SerializeField]
	private Pawn pawn;

	[SerializeField]
	private float detectDistance = 2;

	private ItemController currentSelection;

	private RaycastHit detectHit = new RaycastHit();

	private CharacterManager characterManager;

	private Ray detectRay;

	public void Init (CharacterManager manager)
	{
		characterManager = manager;

		if (pawn.rotateContainer == null)
		{
			Debug.LogError("Missing RotationContariner on Player Detection");
			return;
		}

		detectRay = new Ray(pawn.rotateContainer.position + Vector3.up, pawn.rotateContainer.forward);
	}

	private void Update()
	{
		detectRay.origin = pawn.rotateContainer.position + Vector3.up;
		detectRay.direction = pawn.rotateContainer.forward;

		if (Physics.Raycast(detectRay, out detectHit, detectDistance))
		{
			ItemController shopController = detectHit.collider.GetComponent<ItemController>();

			if (currentSelection)
			{
				currentSelection.Deselect();
				currentSelection = null;
			}

			if (shopController != null)
			{

				currentSelection = shopController;
				currentSelection.Select();
			}
		}
		else
		{
			if (currentSelection)
			{
				currentSelection.Deselect();
				currentSelection = null;
			}
		}
	}

	public void Detect()
	{
		if(currentSelection != null)
		{
			currentSelection.StartInteract(characterManager, () => { });
		}
	}
}
