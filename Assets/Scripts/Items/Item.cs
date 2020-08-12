using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Item")]
public class Item : ScriptableObject
{
    public string name;
    public string id;
    public GameObject itemPrefab;

    private ItemController controller;

    private PlayerManager owner;

    public void Init (Vector3 position, PlayerManager playerManager)
    {
        controller = Instantiate(itemPrefab, position, Quaternion.identity).GetComponent<ItemController>();
        controller.Init(this, playerManager);
        owner = playerManager;
    }
}
