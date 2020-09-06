using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour, IInteractable
{
    [Header("Shop Data")]
    [SerializeField]
    private ShopData shopData;

    [SerializeField]
    private ParticleSystem cashParticle;

    [Header("Interaction")]
    [SerializeField]
    private Transform interactPosition;

    public Action onTaskFinished { get; set; }

    public bool occupied { get; set; }

    public Transform InteractPosition { get { return interactPosition; } }

    private Shop shop;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        shop = new Shop(shopData);
    }

    public void Interact(CharacterManager manager, Action onFinishInteraction = null)
    {
        TruckInventroy inventroy = manager.GetComponent<TruckInventroy>();

        if (inventroy)
        {
            inventroy.AddItem(shop.GetItem());
            cashParticle?.Play();
        }
    }
}
