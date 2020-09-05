using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour, IInteractable
{
    public Item item { get; private set; }
    public GameObject artContainer;

    [SerializeField]
    private Transform interactPosition;

    public Transform InteractPosition { get { return interactPosition; } }
    public CharacterManager owner { get; set; }
    public bool occupied { get; set; }

    public Action onTaskFinished { get; set; }

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


    public virtual void Interact(CharacterManager manager, System.Action onFinishInteraction = null)
    {
        if (occupied)
        {
            onFinishInteraction?.Invoke();
            return;
        }

        occupied = true;
        onTaskFinished = onFinishInteraction;
    }

    public void Delete()
    {
        if (Application.isPlaying)
            Destroy(gameObject);
        else
            DestroyImmediate(gameObject);
    }

    /// <summary>
    /// Called when the item is finished being used
    /// </summary>
    public void EndTask()
    {
        if (onTaskFinished != null)
            onTaskFinished();

        occupied = false;
        onTaskFinished = null;
    }
}
