using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[AddComponentMenu("Item Controller/Hospitality Item")]

public class HospitalityItemController : TimelineItemController
{
	[Header("Hospitality Item")]
	[SerializeField]
	private AIStatTypes statType;

	[SerializeField]
	[Range(0 , 100)]
	private int statInteractionBoost = 10;

	[SerializeField]
	private bool hasQuantity;

	[SerializeField]
	private int startQuantity = 50;

	[SerializeField]
	private GameObject outOfStockContainer;

	private int quantity;
	private bool outOfStock;

	public override void Init(ItemData newItem, HouseManager manager, CharacterManager characterManager = null)
	{
		base.Init(newItem, manager);
		quantity = startQuantity;

		if(outOfStockContainer != null)
			outOfStockContainer?.SetActive(false);
	}

	public override void AddToInventory()
	{
		HouseOwner.HouseInventory.Add(statType, this, quantity);
	}

	public override void RemoveFromInventory()
	{
		HouseOwner.HouseInventory.Add(statType, this, quantity);
	}

	public override void EndInteract()
	{
		base.EndInteract();

		if(!outOfStock)
		{
			quantity--;
			HouseOwner.HouseInventory.Remove(statType, this, 1);

			foreach (ItemOccupant man in Characters)
			{
				(man.manager as AIManager).Stats.Add(statType, statInteractionBoost);
			}

			HouseOwner.AddPP(placePoints, transform.position);

			if (hasQuantity && quantity <= 0)
			{
				outOfStockContainer.SetActive(true);
				outOfStock = true;
			}
		}
	}
}
