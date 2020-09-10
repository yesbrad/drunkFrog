using UnityEngine;

[AddComponentMenu("Item Controller/Static Item")]
public class StaticItemController : ItemController
{
    [Header("Static Item")]
    [SerializeField]
    protected int placePoints = 1;

    public override void OnPlace(Vector3 position, Quaternion rot)
    {
        base.OnPlace(position, rot);
        Owner.AddPP(placePoints);
        AddToInventory();
    }

    public override void OnPickup()
    {
        base.OnPickup();
        Owner?.RemovePP(placePoints);
        RemoveFromInventory();
    }

    public virtual void AddToInventory()
    {
        Owner.HouseInventory?.Add(AIStatTypes.Fun, this, 1);
    }

    public virtual void RemoveFromInventory()
    {
        Owner.HouseInventory?.Remove(AIStatTypes.Fun, this, 1);
    }
}
    