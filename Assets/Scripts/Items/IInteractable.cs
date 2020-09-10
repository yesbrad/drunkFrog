
using UnityEngine;

public interface IInteractable
{
    string Name { get; }
    bool occupied { get; set; }
    System.Action onTaskFinished { get; set; }

    Transform InteractPosition { get; }

    /// <summary>
    /// Entry point for Interactions
    /// </summary>
    void StartInteract(CharacterManager manager, System.Action onFinishInteraction = null);
}
