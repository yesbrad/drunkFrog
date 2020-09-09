using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckItemController : ItemController
{
	private TruckInventroy truckInventroy;

	public override void Init(Item newItem, CharacterManager manager)
	{
		base.Init(newItem, manager);
	}

	public override void StartInteract(CharacterManager manager, Action onFinishInteraction = null)
	{
		base.StartInteract(manager, onFinishInteraction);

		if(truckInventroy == null)
			truckInventroy = manager.GetComponentInParent<TruckInventroy>();


		if (manager.InventoryManager.HasItem() == false)
		{
			Item newItem = truckInventroy.GetItem(CharacterManager);
			manager.InventoryManager.GiveItem(newItem);
		}
	}
}
