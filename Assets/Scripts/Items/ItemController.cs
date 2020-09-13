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
    public HouseManager HouseOwner { get; private set; }
    public CharacterManager CharacterOwner { get; private set; }
    public CharacterManager LastUsedCharacter { get; private set; }


    public bool occupied { get; set; }
    public ItemData item { get; private set; }
    public Action onTaskFinished { get; set; }

    public string Name { get { return item.name; } }

    public virtual void Init (ItemData newItem, HouseManager manager, CharacterManager characterManager = null)
    {
        item = newItem;
        HouseOwner = manager;
        CharacterOwner = characterManager;

        gameObject.SetActive(false);    
    }

    public virtual void StartInteract(CharacterManager manager, System.Action onFinishInteraction)
    {
        if (occupied)
        {
            onFinishInteraction.Invoke();
            return;
        }

        onTaskFinished = onFinishInteraction;
        occupied = true;
        LastUsedCharacter = manager;
    }

    public virtual void EndInteract()
    {
        onTaskFinished.Invoke();
        onTaskFinished = null;
        occupied = false;
    }

    public virtual void OnPickup()
    {
        Debug.Log("Pickup");


        if (occupied)
        {
            Debug.Log("Ending Ineract Early");
            EndInteract();
        }

        gameObject.SetActive(false);
    }

    public virtual void OnPlace(Vector3 position, Quaternion rot)
    {
        transform.position = position;
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

    [ContextMenu("Create Interact Position")]
    private void CreateInteractPosition()
    {
        interactPosition = new GameObject().transform;
        interactPosition.gameObject.name = "InteractPosition";
        interactPosition.parent = transform;
    }

    private void OnDrawGizmos()
    {
        if(showPrefabGizmos)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(Vector3.zero, Vector3.back * 8);
            Gizmos.DrawWireSphere(Vector3.back * 8, 1);

            if(interactPosition != null)
                Gizmos.DrawSphere(interactPosition.position, 0.2f);

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
