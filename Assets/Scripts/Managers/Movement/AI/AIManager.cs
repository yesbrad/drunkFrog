using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AITask
{
    public Interactable item;
    public bool isComplete;

    private Pawn currentPawn;

    [SerializeField] private System.Action onFinished;

    private AIController controller;

    public AITask (Interactable newItem) {
        item = newItem;
    }

    public virtual void OnStart(Pawn pawn, AIController iController, System.Action onFinish)
    {
        currentPawn = pawn;
        onFinished = onFinish;
        controller = iController;
        controller.SetDestination(item.transform.position, OnDestinationReached, item);
        isComplete = false;
    }

    public void OnDestinationReached ()
    {
        item.Interact(currentPawn, () => OnFinish());
        //OnFinish();
    }

    public void OnFinish ()
    {
        //Debug.Log($"Finished Using Item Invokein OnFinish: {item.UUID}", item.controller.gameObject);
        isComplete = true;
        onFinished.Invoke();
        onFinished = null;
    }
}

public class AIManager : CharacterManager
{
    public AITask currentTask;

    private AIController controller;

    public override void Init(HouseManager initialHouse)
    {
        base.Init(initialHouse);
        controller = GetComponentInChildren<AIController>();
        StartTask(GenerateNewTask());
    }

    public AITask GenerateNewTask ()
    {
        AITask newTask = null;
        // Dierent Function for getting a certin type of task
        newTask = GetRandomObjectTask();
        currentTask = newTask;

        //Debug.Log($"NEW TASK POS: {currentTask.item.Position} : NEW Tasnk: {currentTask.item.UUID}");

        return currentTask;
    }

    private AITask GetRandomObjectTask ()
    {
        return new AITask(HouseManager.GetRandomItem().controller);
    }

    public void StartTask(AITask task)
    {
        task.OnStart(Pawn, controller, () => StartTask(GenerateNewTask()));
    }

}
