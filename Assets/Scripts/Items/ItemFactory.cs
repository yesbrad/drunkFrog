using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory : MonoBehaviour
{
	public static Item CreateItem (ItemData data, CharacterManager manager)
	{
		ItemController controller = data.SpawnController();
		return new Item(data, controller, manager, null, false);
    }

	public static Item CloneItem(ItemData data, ItemController controller, CharacterManager manager, Dictionary<string, int> extraData = null)
	{
		return new Item(data, controller, manager, extraData, false);
	}
}
