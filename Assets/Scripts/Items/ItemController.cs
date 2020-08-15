using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public Item item { get; private set; }

    public CharacterManager owner { get; private set; }
    private HouseManager currentHouse;

    public System.Action onTaskFinished;

    private bool occupied;

    public virtual void Init (Item newItem, CharacterManager manager)
    {
        item = newItem;
        owner = manager;
        currentHouse = owner.HouseManager;
    }

    public virtual void Interact(Pawn pawn, System.Action onFinishInteraction = null)
    {
        if (occupied) return; // Early Out if in use.

        occupied = true;
        onTaskFinished = onFinishInteraction;
    }

    public virtual void EndTask ()
    {
        if(onTaskFinished != null)
            onTaskFinished();

        occupied = false;
        onTaskFinished = null;
    }
}
