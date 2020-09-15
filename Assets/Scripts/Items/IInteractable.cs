using System.Collections.Generic;
using UnityEngine;

public struct ItemOccupant
{
    public CharacterManager manager;
    public System.Action onFinished;

    public ItemOccupant(CharacterManager manager, System.Action onFinished)
    {
        this.manager = manager;
        this.onFinished = onFinished;
    }
}

public interface IInteractable
{
    Queue<ItemOccupant> Characters { get; }

    bool IsFull();
    bool HasOccupant();
    bool InHand { get; }

    Transform InteractPosition { get; }

    /// <summary>
    /// Entry point for Interactions
    /// </summary>
    void StartInteract(CharacterManager manager, System.Action onFinishInteraction = null);

    int onRouteAI { get; }
    bool MaxCharactersOnRoute();
    void SetOnRouteAI(int amt);
}
