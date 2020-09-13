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
        HouseOwner.AddPP(placePoints, position);
        AddToInventory();
    }

    public override void OnPickup()
    {
        base.OnPickup();
        HouseOwner?.RemovePP(placePoints, transform.position);
        RemoveFromInventory();
    }

    public virtual void AddToInventory()
    {
        HouseOwner.HouseInventory?.Add(AIStatTypes.Boardness, this, 1);
    }

    public virtual void RemoveFromInventory()
    {
        HouseOwner.HouseInventory?.Remove(AIStatTypes.Boardness, this, 1);
    }
}
    