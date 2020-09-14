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
		public int amount = 0;
		public List<ItemController> items = new List<ItemController>();

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

	public void Add(AIStatTypes catagory, ItemController controller, int amount)
	{
		for (int i = 0; i < categorys.Count; i++)
		{
			if(catagory == categorys[i].catagory)
			{
				if (!categorys[i].items.Contains(controller))
				{
					categorys[i].items.Add(controller);
				}

				categorys[i].amount += amount;
			}
		}
	}

	public void Remove(AIStatTypes catagory, ItemController controller, int amount)
	{
		for (int i = 0; i < categorys.Count; i++)
		{
			if (catagory == categorys[i].catagory)
			{
				categorys[i].amount -= amount;

				if(categorys[i].amount < 0)
					categorys[i].items.Remove(controller);
			}
		}
	}

	/// <summary>
	/// Find Active Item Controller in Inventory
	/// </summary>
	public ItemController FindItem (AIStatTypes catagory)
	{
		foreach (Category i in categorys)
		{
			if(i.catagory == catagory)
			{
				// Handle edgecase of only having one item
				if (i.items.Count < 2)
					return i.items[0];

				ItemController controller = i.items[Random.Range(0, i.items.Count - 1)];

				while (controller.InHand)
				{
					controller = i.items[Random.Range(0, i.items.Count - 1)];
				}

				return controller;
			}
		}

		return null;
	}

	public ItemController FindRandomFunItem()
	{
		return FindItem(AIStatTypes.Boardness);
	}
}
