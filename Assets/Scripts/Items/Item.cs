using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    [SerializeField]
    private ItemData itemData;

    internal ItemController controller;
    public Vector3 Position { get { return controller.transform.position; } }

    protected HouseManager owner;

    internal bool isPlaced;

    public bool Initialized { get; private set; }
    public ItemData Data { get { return itemData; } }

    public string UUID { get; private set; }

    public Dictionary<string, int> extraData;

    public Item(ItemData data, ItemController newController, HouseManager houseManager = null, Dictionary<string, int> extraData = null)
    {
        this.itemData = data;
        this.controller = newController;
        this.controller.Init(this, houseManager);
        this.owner = houseManager;
        this.UUID = $"{newController.transform.position}:{itemData.id}:{Random.Range(0f, 1f)}";
        this.Initialized = true;
        this.extraData = extraData;
    }

    public void Reset()
    {
        itemData = null;
        UUID = "";
        controller = null;
        Initialized = false;
    }

    public virtual void Interact (CharacterManager manager, System.Action onFinishInteraction = null)
    {
        controller.StartInteract(manager, onFinishInteraction);
    }

    public void OnPickup()
    {
        controller.OnPickup();
    }

    public void OnPlace(Vector3 position, Quaternion rotation)
    {
        controller.OnPlace(position, rotation);
    }
}
