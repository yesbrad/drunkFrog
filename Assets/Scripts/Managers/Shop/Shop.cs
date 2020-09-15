using UnityEngine;

public class Shop
{
	private ShopData shopData;

	private int quantity;

	public Shop (ShopData shopData)
	{
		this.shopData = shopData;
		quantity = shopData.amount;
	}
	
	public ItemData GetItem(PlayerManager customer)
	{
		if(customer == null)
		{
			Debug.LogError("Cutomer Is Null!");
			return null;
		}

		if(shopData.cost > customer.Cash)
		{
			Debug.Log("NoMoneyToBuy");
			return null;
		}

		if(quantity <= 0)
		{
			Debug.Log("None of this item left!");
			return null;
		}

		customer.AddCash(-shopData.cost);
		quantity--;

		return shopData.item;
	}
}
