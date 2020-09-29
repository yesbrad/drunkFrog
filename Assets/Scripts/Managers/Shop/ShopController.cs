using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    [Header("Shop")]
    [SerializeField]
    private ShopData shopData;

    [SerializeField] TMPro.TextMeshProUGUI nameText;
    [SerializeField] TMPro.TextMeshProUGUI costText;

    [SerializeField]
    private ParticleSystem cashParticle;

    [SerializeField]
    private Transform holdItemSpawn;

    private Shop shop;

    private void Awake()
    {
        shop = new Shop(shopData);

        if (holdItemSpawn)
        {
            Instantiate(shopData.item.itemPrefab.GetComponent<ItemController>().holdItem, holdItemSpawn.position, Quaternion.identity).GetComponent<ItemController>().holdItem.ShowItem();
        }

        nameText.SetText(shopData.item.name);
        costText.SetText($"${shopData.cost}");
    }

    public void StartInteract(CharacterManager manager)
    {
        TruckInventroy inventroy = manager.InitialHouse.TruckInventroy;
        PlayerManager playerManager = manager.GetComponent<PlayerManager>();

        if (inventroy)
        {
            ItemData possibleData = shop.GetItem(playerManager);

            if(possibleData != null)
            {
                inventroy.AddItem(possibleData);
                cashParticle?.Play();
            }
        }
    }
}
