using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    private Item item;

    private PlayerManager owner;
    private HouseManager currentHouse;

    public System.Action onTaskFinished;

    private bool occupied;


    public void Init (Item newItem, PlayerManager manager)
    {
        item = newItem;
        owner = manager;
        currentHouse = owner.HouseManager;
    }

    public virtual void Interact(Pawn pawn, System.Action taskFinishCallback)
    {
        if (occupied) return; // Early Out if in use.

        occupied = true;
        onTaskFinished = taskFinishCallback;

        //Do what ever needs to be done
        Debug.Log("Interacting!");

        owner.HouseManager.AddHP(item.basePlacePoints);
    }

    public void EndTask ()
    {
        if(onTaskFinished != null)
            onTaskFinished();

        occupied = false;
        onTaskFinished = null;
    }

    public virtual void Use(Pawn pawn)
    {
        Debug.Log("YES FUCK ME");
    }
}
