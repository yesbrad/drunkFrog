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

    public ItemController controller { get; private set; }
    public Vector3 Position { get { return controller.transform.position; } }

    private CharacterManager owner;
    public string UUID { get; private set; }

    public virtual void Init (Vector3 position, CharacterManager playerManager)
    {
        controller = Instantiate(itemPrefab, position, Quaternion.identity).GetComponent<ItemController>();
        controller.Init(this, playerManager);
        owner = playerManager;
        UUID = $"{position}:{playerManager.Pawn.Position}:{id}:{Random.Range(0f,1f)}";
    }

    public virtual void Use (Pawn pawn)
    {
        controller.Use(pawn);
    }
}
