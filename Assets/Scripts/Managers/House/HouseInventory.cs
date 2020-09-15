using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

[RequireComponent(typeof(HouseManager))]
public class HouseInventory : MonoBehaviour
{
	[System.Serializable]
	public class Category
	{
		public AIStatTypes catagory;
		public int Amount
		{
			get
			{
				int amt = 0;

				foreach (HouseItemController item in items)
				{
					amt += item.quantity;
				}

				return amt;
			}
		}

		public List<HouseItemController> items = new List<HouseItemController>();

		public Category (AIStatTypes catagory)
		{
			this.catagory = catagory;
		}
	}

	public List<Category> categorys = new List<Category>();

	private void Awake()
	{
		categorys.Add(new Category(AIStatTypes.Soberness));
		categorys.Add(new Category(AIStatTypes.Hunger));
		categorys.Add(new Category(AIStatTypes.Boardness));
		categorys.Add(new Category(AIStatTypes.Thirst));
	}

	public void Add(AIStatTypes catagory, HouseItemController controller, int amount)
	{
		for (int i = 0; i < categorys.Count; i++)
		{
			if(catagory == categorys[i].catagory)
			{
				if (!categorys[i].items.Contains(controller))
				{
					categorys[i].items.Add(controller);
				}
			}
		}
	}

	public void Remove(AIStatTypes catagory, HouseItemController controller, int amount)
	{
		for (int i = 0; i < categorys.Count; i++)
		{
			if (catagory == categorys[i].catagory)
			{
				categorys[i].items.Remove(controller);
			}
		}
	}

	/// <summary>
	/// Find Active Item Controller in Inventory
	/// </summary>
	public HouseItemController FindItem (AIStatTypes catagory)
	{
		foreach (Category i in categorys)
		{
			if(i.catagory == catagory)
			{
				if(i.items == null || i.items.Count == 0)
				{
					return null;
				}

				// Handle edgecase of only having one item
				if (i.items.Count < 2)
					return i.items[0];

				HouseItemController controller = i.items[Random.Range(0, i.items.Count)];

				bool noAvailableItems = true;

				foreach (HouseItemController item in i.items)
				{
					if (!item.MaxCharactersOnRoute() && !item.InHand)
						noAvailableItems = false;
				}

				if (noAvailableItems)
				{
					return null;
				}

				while (controller.InHand || controller.MaxCharactersOnRoute())
				{
					controller = i.items[Random.Range(0, i.items.Count)];
				}

				if (controller.MaxCharactersOnRoute())
				{
					print("Shess sofucking coookedddd plz help");
				}

				return controller;
			}
		}

		return null;
	}

	public HouseItemController FindRandomFunItem()
	{
		return FindItem(AIStatTypes.Boardness);
	}
}
