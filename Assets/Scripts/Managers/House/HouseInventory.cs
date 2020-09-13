using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

[RequireComponent(typeof(HouseManager))]
public class HouseInventory : MonoBehaviour
{
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
				categorys[i].items.Add(controller);
				categorys[i].amount += amount;
			}
		}
	}

	public ItemController FindItem (AIStatTypes catagory)
	{
		foreach (Category i in categorys)
		{
			if(i.catagory == catagory)
			{
				return i.items[Random.Range(0, i.items.Count - 1)];
			}
		}

		return null;
	}

	public ItemController FindRandomFunItem()
	{
		if (categorys[2].items.Count <= 0)
		{
			Debug.Log("WEARE NUL");
			return null;
		}

		return categorys[2].items[Random.Range(0, categorys[2].items.Count - 1)];
	}
}
