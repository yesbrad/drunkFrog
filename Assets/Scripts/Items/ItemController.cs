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

        gameObject.SetActive(false);    
    }

    public void OnPickup()
    {
        Debug.Log("OnPicku[p", gameObject);
        gameObject.SetActive(false);
    }


    // On Place must be call to position the item controller
    public void OnPlace(Vector3 position, Quaternion rot)
    {
        transform.position = position;
        
        if(item.Data.size != 1)
            transform.rotation = rot;

        // Use this so the player can customize rotaions
        artContainer.transform.rotation = Quaternion.identity;

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
