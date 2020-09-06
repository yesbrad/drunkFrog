using UnityEngine;

public class Shop
{
	private ShopData shopData;

	public Shop (ShopData shopData)
	{
		this.shopData = shopData;
	}
	
	public ItemData GetItem()
	{
		// TODO: CheckCost
		// TODO: Check Amount
		return shopData.item;
	}
}
