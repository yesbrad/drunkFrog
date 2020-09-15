using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[AddComponentMenu("Item Controller/Hospitality Item")]

public class HouseItemController : TimelineItemController
{
	[Header("Stat Item")]
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

	private bool outOfStock;

	public int quantity { get; set; }

	public override void Init(HouseManager manager, CharacterManager characterManager = null)
	{
		base.Init(manager);
		quantity = startQuantity;

		if(outOfStockContainer != null)
			outOfStockContainer?.SetActive(false);
	}

	public virtual void AddToInventory()
	{
		HouseOwner.HouseInventory.Add(statType, this, quantity);
	}

	public virtual void RemoveFromInventory()
	{
		HouseOwner.HouseInventory.Remove(statType, this, quantity);
	}
	public override void OnPickup()
	{
		base.OnPickup();
		RemoveFromInventory();
	}

	public override void OnPlace(Vector3 position, Quaternion rot)
	{
		base.OnPlace(position, rot);
		AddToInventory();
	}

	public override void EndInteract()
	{
		base.EndInteract();

		if(!outOfStock)
		{

			foreach (ItemOccupant man in Characters)
			{
				(man.manager as AIManager).Stats.Add(statType, statInteractionBoost);
			}

			HouseOwner.AddPP(placePoints, transform.position);

			if (hasQuantity)
			{
				quantity--;
				HouseOwner.HouseInventory.Remove(statType, this, 1);

				if (quantity <= 0)
				{
					outOfStockContainer.SetActive(true);
					outOfStock = true;
				}
			}

		}
	}
}
