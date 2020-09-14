using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : MonoBehaviour, IInteractable
{
    [SerializeField]
    private float shrinkageTime = 5;

    [SerializeField]
    [Range(2, 20)]
    private int groupLimit = 5;

    [SerializeField]
    [Range(10, 100)]
    [Tooltip("Seconds")]
    private int maxTime = 60;

    [SerializeField]
    private Transform interactPosition;

    private float currentBootTime;

    public Queue<ItemOccupant> Characters { get; private set; }
    public Transform InteractPosition { get { return interactPosition; } }
    public string Name { get { return "Group"; } }
    public bool IsFull() => Characters.Count > groupLimit;
    public bool HasOccupant() => Characters.Count > 0;
    public bool InHand { get { return false; } }

    private void Awake()
    {
        Characters = new Queue<ItemOccupant>();
        currentBootTime = shrinkageTime;
    }

    public void StartInteract(CharacterManager manager, System.Action onFinishInteraction)
    { 
        if(Characters.Count > groupLimit)
        {
            onFinishInteraction();
            return;
        }

        ItemOccupant item = new ItemOccupant(manager, onFinishInteraction);
       // item.manager.Pawn.SetState(Pawn.PawnState.Talking);
        Characters.Enqueue(item);
    }

    private void Update()
    {
        currentBootTime -= Time.deltaTime;

        if(currentBootTime < 0)
        {
            if(Characters.Count > 0)
            {
                BootAI(Characters.Dequeue());
            }

            currentBootTime = shrinkageTime;
        }

    }

    public void BootAI (ItemOccupant occupant, int index = 0)
    {
        occupant.onFinished.Invoke();
        occupant.manager.Pawn.SetState(Pawn.PawnState.Free);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 3);
    }

    
}
