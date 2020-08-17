using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    internal bool occupied;
    public System.Action onTaskFinished;
    public CharacterManager owner { get; set; }

    public virtual void Interact(Pawn pawn, System.Action onFinishInteraction = null)
    {
        if (occupied) // Early Out if in use.
        {
            onFinishInteraction();
            return;
        }

        occupied = true;
        onTaskFinished = onFinishInteraction;
    }

    public virtual void EndTask()
    {
        if (onTaskFinished != null)
            onTaskFinished();

        occupied = false;
        onTaskFinished = null;
    }
}
