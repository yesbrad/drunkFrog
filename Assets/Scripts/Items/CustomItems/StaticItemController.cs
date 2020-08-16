using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StaticItemController : ItemController
{
    public int placePoints;

    public override void Init(Item newItem, CharacterManager manager)
    {
        base.Init(newItem, manager);
    }

    public override void Interact(Pawn pawn, Action taskFinishCallback)
    {
        base.Interact(pawn, taskFinishCallback);
        owner.HouseManager.AddHP(placePoints);
        StartCoroutine(WaitEnd());
    }

    public IEnumerator WaitEnd ()
    {
        yield return new WaitForSeconds(1);
        EndTask();
        yield return null;
    }

    public override void EndTask()
    {
        base.EndTask();
    }
}
