using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : Interactable
{
    public Item item { get; private set; }

    public virtual void Init (Item newItem, CharacterManager manager)
    {
        item = newItem;
        owner = manager;
    }

    public override void Interact(Pawn pawn, System.Action onFinishInteraction = null)
    {
        base.Interact(pawn, onFinishInteraction);
    }

    public override void EndTask()
    {
        base.EndTask();
    }
}
