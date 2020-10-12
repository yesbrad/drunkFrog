using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Item Controller/Base Item")]
public class ItemController : MonoBehaviour, IInteractable, IDetection
{
    [Header("Gizmos")]
    [SerializeField]
    private bool showPrefabGizmos;

    //[Header("Art")]
    protected ItemArt itemArt;

    [Header("Base Item")]
    [SerializeField]
    private Transform interactPosition;

    [Space()]
    [SerializeField]
    private int maxOccupants = 1;
    private int maxOnRoute = 1;

    public Transform InteractPosition { get { return interactPosition; } }
    public HouseManager HouseOwner { get; private set; }
    public CharacterManager CharacterOwner { get; private set; }

    public Queue<ItemOccupant> Characters { get; private set; }
    public bool IsFull() => Characters.Count >= maxOccupants;
    public bool HasOccupant() => Characters.Count > 0;

    public int onRouteAI { get; private set; }
    public bool MaxCharactersOnRoute() => onRouteAI > maxOnRoute;
    public void SetOnRouteAI (int amt) => onRouteAI += amt;

    public bool InHand { get; private set; }

    private void OnEnable()
    {
        Characters = new Queue<ItemOccupant>();
    }

    public virtual void Init ( HouseManager manager, CharacterManager characterManager = null)
    {
        HouseOwner = manager;
        CharacterOwner = characterManager;
        gameObject.SetActive(false);
        InHand = false;
        itemArt = GetComponentInChildren<ItemArt>();
    }

    public virtual void StartInteract(CharacterManager manager, System.Action onFinishInteraction)
    {
        if (IsFull())
        {
            onFinishInteraction.Invoke();
            return;
        }

        Characters.Enqueue(new ItemOccupant(manager, onFinishInteraction));
    }

    public virtual void EndInteract()
    {
        ClearPlayers();
    }

    public virtual void ClearPlayers()
    {
        foreach (ItemOccupant character in Characters)
        {
            character.onFinished.Invoke();
        }

        Characters.Clear();
    }

    public virtual void OnPickup()
    {
        ClearPlayers();
        gameObject.SetActive(false);
        InHand = true;
    }

    public virtual void OnPlace(Vector3 position, Quaternion rot)
    {
        transform.position = position;
        transform.rotation = rot;
        gameObject.SetActive(true);
        InHand = false;
    }

    public void Select ()
    {
        itemArt?.SetSelection(true);
    }

    public void Deselect()
    {
        itemArt?.SetSelection(false);
    }

    public void ResetPawns()
    {
        foreach (ItemOccupant itemOccupant in Characters)
        {
            itemOccupant.manager.Pawn.EndTimeline();
            itemOccupant.manager.Pawn.LockPawn(false);
        }
    }

    [EasyButtons.Button("Validate Controller")]
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
