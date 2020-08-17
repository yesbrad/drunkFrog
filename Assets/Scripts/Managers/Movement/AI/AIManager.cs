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

    public Vector3 destination;

    public AITask (Interactable newItem) {
        item = newItem;
        destination = item.transform.position;
    }

    public AITask(Vector3 positon)
    {
        destination = positon;
    }

    public virtual void OnStart(Pawn pawn, AIController iController, System.Action onFinish)
    {
        currentPawn = pawn;
        onFinished = onFinish;
        controller = iController;
        controller.SetDestination(destination, OnDestinationReached, item);
        isComplete = false;
    }

    public void OnDestinationReached ()
    {
        if (item)
        {
            item.Interact(currentPawn, () => OnFinish());
        } else
        {
            OnFinish();
        }
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
        currentTask = SelectTask();
        return currentTask;
    }

    public AITask SelectTask ()
    {
        if (CurrentGrid != null)
        {
            return GetRandomPositionTask();
        }

        return GetCentrePointTask();
    }

    private AITask GetRandomObjectTask ()
    {
        return new AITask(HouseManager.GetRandomItem().controller);
    }

    private AITask GetRandomPositionTask ()
    {
        return new AITask(CurrentGrid.GetRandomPosition());
    }
    private AITask GetCentrePointTask()
    {
        return new AITask(HouseManager.GetCenterPoint());
    }
    public void StartTask(AITask task)
    {
        task.OnStart(Pawn, controller, () => StartTask(GenerateNewTask()));
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(currentTask.destination, Vector3.one);
    }
}
