using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Item Controller/Base Item")]
public class ItemController : MonoBehaviour, IInteractable
{
    [Header("Gizmos")]
    [SerializeField]
    private bool showPrefabGizmos;

    [Header("Base Item")]
    [SerializeField]
    private Transform interactPosition;

    public Transform InteractPosition { get { return interactPosition; } }
    public HouseManager Owner { get; private set; }
    public CharacterManager LastUsedCharacter { get; private set; }

    public bool occupied { get; set; }
    public Item item { get; private set; }
    public Action onTaskFinished { get; set; }

    public string Name { get { return item.Data.name; } }

    public virtual void Init (Item newItem, HouseManager manager)
    {
        item = newItem;
        Owner = manager;
        gameObject.SetActive(false);    
    }

    public virtual void StartInteract(CharacterManager manager, System.Action onFinishInteraction = null)
    {
        if (occupied)
        {
            onFinishInteraction?.Invoke();
            return;
        }

        onTaskFinished = onFinishInteraction;

        occupied = true;

        LastUsedCharacter = manager;
    }

    public virtual void EndInteract()
    {
        onTaskFinished?.Invoke();
        occupied = false;
        onTaskFinished = null;
    }

    public virtual void OnPickup()
    {
        gameObject.SetActive(false);
    }

    public virtual void OnPlace(Vector3 position, Quaternion rot)
    {
        transform.position = position;
        
        if(item.Data.size.IsSingle() == false)
            transform.rotation = rot;

        gameObject.SetActive(true);
    }

    [ContextMenu("Validate Controller")]
    public virtual void Validate()
    {
        if (GetComponents<ItemController>().Length > 1)
        {
            Debug.LogError("Item Controller has more than one item conroller on it");
        }

        if (interactPosition == null)
        {
            Debug.LogError("Item Controller is missing interact position!", gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        if(showPrefabGizmos)
        {
            Gizmos.color = Color.cyan;

            for (int i = 0; i < 10; i++)
            {
                for (int x = 0; x < 10; x++)
                {
                    Gizmos.DrawWireCube(new Vector3(i * constants.GridCellSize, 0, x * constants.GridCellSize), new Vector3(constants.GridCellSize,0,constants.GridCellSize));
                }
            }
        }
    }
}
