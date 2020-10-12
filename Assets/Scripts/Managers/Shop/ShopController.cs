using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : ItemController
{
    [Header("Shop")]
    [SerializeField]
    public ShopData shopData;

    [SerializeField] TMPro.TextMeshProUGUI nameText;
    [SerializeField] TMPro.TextMeshProUGUI costText;

    [SerializeField] TMPro.TextMeshProUGUI stockText;

    [SerializeField]
    private Transform holdItemSpawn;

    private Shop shop;

    private void Awake()
    {
        shop = new Shop(shopData);

        //if (holdItemSpawn)
        //{
        //    HoldItem item = Instantiate(shopData.item.itemPrefab.GetComponent<ItemController>().itemArt, holdItemSpawn.position, Quaternion.identity).GetComponent<HoldItem>();
        //    item.showOnAwake = true;
        //    item.ShowItem();
        //}

        if(shopData.item.itemPrefab.GetComponent<ItemArt>() != null)
        {
            Instantiate(shopData.item.itemPrefab.GetComponent<ItemArt>().gameObject, holdItemSpawn.position, Quaternion.identity);  
        }

        nameText.SetText(shopData.item.name);
        costText.SetText($"${shopData.cost}");
        stockText.SetText($"{shopData.amount}/{shopData.amount}");
        itemArt = GetComponentInChildren<ItemArt>();
    }

    public override void StartInteract(CharacterManager manager, System.Action onFinishInteraction)
    {
        base.StartInteract(manager, onFinishInteraction);
        TruckInventroy inventroy = manager.InitialHouse.TruckInventroy;
        PlayerManager playerManager = manager.GetComponent<PlayerManager>();
        
        if (inventroy)
        {
            Shop.ShopResponse boughtItem = shop.GetItem(playerManager);

            switch (boughtItem)
            {
                case Shop.ShopResponse.Success:
                    stockText.SetText($"{shop.quantity}/{shopData.amount}");
                    inventroy.AddItem(shop.shopData.item);
                    PPFXController.instance.Play(PPFXController.PPState.MoneyMinus, transform.position);
                    break;
                case Shop.ShopResponse.OutOfOrder:
                    PPFXController.instance.Play(PPFXController.PPState.outOfOrder, transform.position);
                        break;
            }
        }
    }
}
