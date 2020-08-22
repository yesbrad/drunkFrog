using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : Interactable
{
    public Item item { get; private set; }
    public GameObject artContainer;

    private GameObject boxContainer;
    private bool isBoxed;

    public virtual void Init (Item newItem, CharacterManager manager, bool boxed)
    {
        item = newItem;
        owner = manager;
        isBoxed = boxed;

        if (isBoxed)
        {
            if (artContainer)
                artContainer.SetActive(false);

            //Spawn Box
        } 
    }

    public void OnPickup()
    {
        Destroy(gameObject);
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
