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
		Debug.Log("Begining of thing");

		if(pawn.rotateContainer == null)
		{
			Debug.Log("early out");
			Debug.LogError("Missing RotationContariner on Player Detection");
			return;
		}

		Ray detectRay = new Ray(pawn.rotateContainer.position, pawn.rotateContainer.forward);

		Debug.Log("Begining of Rayvasty");

		if (Physics.Raycast(detectRay, out detectHit, detectDistance))
		{
			ShopController shopController = detectHit.collider.GetComponent<ShopController>();

			Debug.Log(shopController);
			Debug.Log("Middle of thing");

			if (shopController != null)
			{
				shopController.StartInteract(manager);
			}
		}
	}
}
