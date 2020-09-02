using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : Interactable
{
    public Item item { get; private set; }
    public GameObject artContainer;

    private GameObject boxContainer;
    private bool isBoxed;

    private float rotation;

    public virtual void Init (Item newItem, CharacterManager manager, bool inInventory, bool boxed)
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

        if (inInventory)
        {
            gameObject.SetActive(false);
        }
    }

    public void OnPickup()
    {
        Debug.Log("OnPicku[p", gameObject);
        gameObject.SetActive(false);
    }

    public void OnPlace(Vector3 position, Quaternion rot)
    {
        transform.position = position;
        transform.rotation = rot;
        gameObject.SetActive(true);
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
