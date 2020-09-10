using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("Item Controller/Hospitality Item")]

public class HospitalityItemController : TimelineItemController
{
	[Header("Hospitality Item")]
	private AIStatTypes statType;

	[SerializeField]
	private int startQuantity = 50;

	[SerializeField]
	private GameObject outOfStockContainer;

	private int quantity;
	private bool outOfStock;

	public override void Init(Item newItem, HouseManager manager)
	{
		base.Init(newItem, manager);
		quantity = startQuantity;
	}

	public override void AddToInventory()
	{
		Owner.HouseInventory.Add(statType, this, quantity);
	}

	public override void RemoveFromInventory()
	{
		Owner.HouseInventory.Add(statType, this, quantity);
	}

	public override void EndInteract()
	{
		base.EndInteract();

		if(!outOfStock)
		{
			quantity--;
			Owner.HouseInventory.Remove(statType, this, 1);

			if(quantity <= 0)
			{
				outOfStockContainer.SetActive(true);
				outOfStock = true;
			}
		}
	}
}
