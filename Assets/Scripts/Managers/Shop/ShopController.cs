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

    public bool IsFull() => Characters.Count > 1;
    public bool HasOccupant() => Characters.Count > 0;
    public Transform InteractPosition { get { return interactPosition; } }

    private Shop shop;

    public string Name { get { return shopData.item.name; } }

    public Queue<ItemOccupant> Characters { get; private set; }

    private void Awake()
    {
        Characters = new Queue<ItemOccupant>();
        shop = new Shop(shopData);
    }

    public void StartInteract(CharacterManager manager, Action onFinishInteraction = null)
    {
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
