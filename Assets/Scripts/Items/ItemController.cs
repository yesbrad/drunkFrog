using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour, IInteractable
{

    [SerializeField]
    private GameObject artContainer;

    [SerializeField]
    private Transform interactPosition;

    public Transform InteractPosition { get { return interactPosition; } }
    public CharacterManager CharacterManager { get; set; }
    public bool occupied { get; set; }
    public Item item { get; private set; }
    public Action onTaskFinished { get; set; }

    public virtual void Init (Item newItem, CharacterManager manager)
    {
        item = newItem;
        CharacterManager = manager;
        gameObject.SetActive(false);    
    }

    public virtual void StartInteract(CharacterManager manager, System.Action onFinishInteraction = null)
    {
        onTaskFinished = onFinishInteraction;

        if (occupied)
        {
            EndInteract();
        }

        occupied = true;
    }

    public void EndInteract()
    {
        onTaskFinished?.Invoke();
        occupied = false;
        onTaskFinished = null;
    }

    public void OnPickup()
    {
        gameObject.SetActive(false);
    }

    public void OnPlace(Vector3 position, Quaternion rot)
    {
        transform.position = position;
        
        if(item.Data.size.IsSingle() == false)
            transform.rotation = rot;

        gameObject.SetActive(true);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(Vector3.one * 10, Vector3.one * 20);
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(constants.GridCellSize,0,constants.GridCellSize));
    }
}
