using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
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
