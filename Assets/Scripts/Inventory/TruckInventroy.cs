using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
public class TruckInventroy : MonoBehaviour
{
	public Queue<ItemData> inventory = new Queue<ItemData>();
	public ItemData[] debugInventory;

	private void Awake()
	{
		foreach (ItemData data in debugInventory)
		{
			AddItem(data);
		}
	}

	public void AddItem (ItemData item)
	{
		inventory.Enqueue(item);
	}

	public Item GetItem (HouseManager manager = null)
	{
		if(inventory.Count < 1)
		{
			Debug.Log("No More Item In Truck");
			return null;
		}

		ItemData data = inventory.Dequeue();
		return ItemFactory.CreateItem(data, manager);
	}
}
