using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : Interactable
{
    public Item item { get; private set; }
    public GameObject artContainer;
    public virtual void Init (Item newItem, CharacterManager manager)
    {
        item = newItem;
        owner = manager;
        gameObject.SetActive(false);    
    }
    public void OnPickup()
    {
        gameObject.SetActive(false);
    }

    // On Place must be call to position the item controller
    public void OnPlace(Vector3 position, Quaternion rot)
    {
        transform.position = position;
        
        if(item.Data.size.IsSingle() == false)
            transform.rotation = rot;

        gameObject.SetActive(true);
    }

    public override void Interact(Pawn pawn, System.Action onFinishInteraction = null)
    {
        base.Interact(pawn, onFinishInteraction);
    }

    public void Delete()
    {
        if (Application.isPlaying)
            Destroy(gameObject);
        else
            DestroyImmediate(gameObject);
    }

    public override void EndTask()
    {
        base.EndTask();
    }
}
