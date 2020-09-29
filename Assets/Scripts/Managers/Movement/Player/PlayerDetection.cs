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

	private RaycastHit detectHit = new RaycastHit();

	public void Detect(CharacterManager manager)
	{
		if(pawn.rotateContainer == null)
		{
			Debug.LogError("Missing RotationContariner on Player Detection");
			return;
		}

		Ray detectRay = new Ray(pawn.rotateContainer.position, pawn.rotateContainer.forward);
		
		if(Physics.Raycast(detectRay, out detectHit, detectDistance))
		{
			ShopController shopController = detectHit.collider.GetComponent<ShopController>();

			if(shopController != null)
			{
				shopController.StartInteract(manager);
			}
		}
	}
}
