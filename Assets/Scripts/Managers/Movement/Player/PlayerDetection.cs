using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
public class PlayerDetection : MonoBehaviour
{
	[SerializeField]
	private Transform rotationContainer;

	[SerializeField]
	private float detectDistance = 2;

	private RaycastHit detectHit = new RaycastHit();

	public void Detect(CharacterManager manager)
	{
		if(rotationContainer == null)
		{
			Debug.LogError("Missing RotationContariner on Player Detection");
			return;
		}

		Ray detectRay = new Ray(rotationContainer.position, rotationContainer.forward);
		
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
