using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string name;
    public string id;

    public GameObject itemPrefab;

    internal ItemController controller;
    public Vector3 Position { get { return controller.transform.position; } }

    private CharacterManager owner;
    public string UUID { get; private set; }
    /*
    public void Init (ItemController newController, CharacterManager playerManager)
    {
        controller = newController;// Instantiate(itemPrefab, position, Quaternion.identity).GetComponent<ItemController>();
        controller.Init(this, playerManager);
        owner = playerManager;
        UUID = $"{Position}:{id}:{Random.Range(0f,1f)}";
    }
    */

    // This is a terrible way of making an instance but, it for the protoype.
    public Item Init(ItemController newController, CharacterManager playerManager)
    {
        Item newItem = new Item();
        newItem.name = name;
        newItem.id = id;
        newItem.itemPrefab = itemPrefab;
        newItem.controller = newController;
        newItem.controller.Init(newItem, playerManager);
        newItem.owner = playerManager;
        newItem.UUID = $"{newController.transform.position}:{id}:{Random.Range(0f, 1f)}";
        return newItem;
    }

    public virtual void Interact (Pawn pawn, System.Action onFinishInteraction = null)
    {
        controller.Interact(pawn, onFinishInteraction);
    }
}
