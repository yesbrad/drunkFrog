using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckItemController : ItemController
{
	public override void StartInteract(CharacterManager manager, Action onFinishInteraction = null)
	{
		base.StartInteract(manager, onFinishInteraction);

		if (manager.InventoryManager.HasItem() == false)
		{
			Item newItem = manager.CurrentHouse.TruckInventroy.GetItem(HouseOwner);

			if(newItem != null)
			{
				manager.InventoryManager.GiveItem(newItem);
			}
		}

		EndInteract();
	}
}
