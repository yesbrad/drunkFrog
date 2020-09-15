using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : ItemController
{
    [Header("Shop")]
    [SerializeField]
    private ShopData shopData;

    [SerializeField]
    private ParticleSystem cashParticle;

    private Shop shop;

    private void Awake()
    {
        shop = new Shop(shopData);
    }

    public override void StartInteract(CharacterManager manager, Action onFinishInteraction = null)
    {
        base.StartInteract(manager, onFinishInteraction);

        TruckInventroy inventroy = manager.GetComponent<TruckInventroy>();
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
