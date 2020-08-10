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

    public void Init (Vector3 position)
    {
        controller = Instantiate(itemPrefab, position, Quaternion.identity).GetComponent<ItemController>();
    }
}
