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
        OwnerCharacterManager?.HouseManager?.AddPP(placePoints);
    }
    public override void OnPickup()
    {
        base.OnPickup();
        OwnerCharacterManager?.HouseManager?.RemovePP(placePoints);
    }
}
    