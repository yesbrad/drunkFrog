using System.Collections;
using System.Collections.Generic;

public class ItemFactory
{
	public static Item CreateItem (ItemData data, HouseManager manager)
	{
		ItemController controller = data.SpawnController();
		return new Item(data, controller, manager, null);
    }

	public static Item CloneItem(ItemData data, ItemController controller, HouseManager manager, Dictionary<string, int> extraData = null)
	{
		return new Item(data, controller, manager, extraData);
	}
}
