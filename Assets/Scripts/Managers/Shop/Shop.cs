using UnityEngine;

public class Shop
{
	public enum ShopResponse
	{
		Success,
		OutOfOrder,
		NoFunds
	}

	public ShopData shopData;
	public int quantity;

	public Shop (ShopData shopData)
	{
		this.shopData = shopData;
		quantity = shopData.amount;
	}
	
	public ShopResponse GetItem(PlayerManager customer)
	{
		if(customer == null)
		{
			Debug.LogError("Cutomer Is Null!");
			return ShopResponse.NoFunds;
		}

		if(shopData.cost > customer.Cash)
		{
			Debug.Log("NoMoneyToBuy");
			return ShopResponse.NoFunds;
		}

		if(quantity <= 0)
		{
			Debug.Log("None of this item left!");
			return ShopResponse.OutOfOrder;
		}

		customer.AddCash(-shopData.cost);
		quantity--;

		return ShopResponse.Success;
	}
}
