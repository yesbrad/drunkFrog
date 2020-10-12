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

	private IDetection currentSelection;

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

		detectRay = new Ray(pawn.rotateContainer.position - (Vector3.down * 0.8f), pawn.rotateContainer.forward);
	}

	private void Update()
	{
		detectRay.origin = pawn.rotateContainer.position - (Vector3.down * 0.8f);
		detectRay.direction = pawn.rotateContainer.forward;

		if (Physics.Raycast(detectRay, out detectHit, detectDistance))
		{
			IDetection shopController = detectHit.collider.GetComponent<IDetection>();

			if (currentSelection != null)
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
			if (currentSelection != null)
			{
				currentSelection.Deselect();
				currentSelection = null;
			}
		}
	}

	public void Detect()
	{
		Debug.Log("Detecy Inside");
		if (currentSelection != null)
		{
			Debug.Log("Detecy Inside");
			currentSelection.StartInteract(characterManager, () => { });
		}
	}
}
