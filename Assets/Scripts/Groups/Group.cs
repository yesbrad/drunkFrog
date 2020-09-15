using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : ItemController
{
    [Header("Group")]
    [SerializeField]
    private float shrinkageTime = 5;

    [SerializeField]
    [Range(10, 100)]
    [Tooltip("Seconds")]
    private int maxTime = 60;

    private float currentBootTime;

    private void Awake()
    {
        currentBootTime = shrinkageTime;
    }

    public void StartInteract(CharacterManager manager, System.Action onFinishInteraction)
    { 
        if(true)//Characters.Count > groupLimit)
        {
            onFinishInteraction();
            return;
        }

        ItemOccupant item = new ItemOccupant(manager, onFinishInteraction);
       // item.manager.Pawn.SetState(Pawn.PawnState.Talking);
        Characters.Enqueue(item);
    }

    /*
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
    */

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
