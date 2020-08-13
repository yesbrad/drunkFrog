using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Item")]
public class Item : ScriptableObject
{
    public string name;
    public string id;

    [Header("Points")]
    public int basePlacePoints = 1;

    public GameObject itemPrefab;

    private ItemController controller;

    private CharacterManager owner;

    public virtual void Init (Vector3 position, CharacterManager playerManager)
    {
        controller = Instantiate(itemPrefab, position, Quaternion.identity).GetComponent<ItemController>();
        controller.Init(this, playerManager);
        owner = playerManager;
    }

    public virtual void Use (Pawn pawn)
    {
        controller.Use(pawn);
    }
}
