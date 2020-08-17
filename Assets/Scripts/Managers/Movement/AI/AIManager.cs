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

    public bool isInGroup;

    public AITask (Interactable newItem, bool isGroup = false) {
        item = newItem;
        destination = item.transform.position;
        isInGroup = isGroup;
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

    private Transform debugGroupLocations;

    private int tasksCompleted;

    public bool HasCompletedFirstTask { get { return tasksCompleted > 1; } }

    public override void Init(HouseManager initialHouse)
    {
        base.Init(initialHouse);
        controller = GetComponentInChildren<AIController>();
        StartAndGenerateTask();
    }

    public void StartAndGenerateTask()
    {
        StartTask(GenerateNewTask());
    }

    public AITask GenerateNewTask ()
    {
        currentTask = SelectTask();
        return currentTask;
    }

    public void OverrideCurrentTask(AITask newTask)
    {
        currentTask = newTask;
        currentTask.OnStart(Pawn, controller, () => StartTask(GenerateNewTask()));
    }

    public AITask SelectTask ()
    {
        if (Random.Range(0f, 1f) > 0.6 && HasCompletedFirstTask)
        {
            AITask possibleTask = FindGroup();

            if (possibleTask != null)
            {
                return possibleTask;
            }
            else
            {
               // Debug.Log("Could Not Find Group");

                if (Random.Range(0f, 1f) > 0.7)
                {

                    Debug.Log("ATTEMPONIG a GROUP!", gameObject);

                    AITask possibleTaskCreate = CreateBasicGroup();

                    if (possibleTaskCreate != null)
                    {
                        Debug.Log("Were creating a GROUP!", gameObject);
                        return possibleTaskCreate;
                    }
                }
            }
        }

        if (Random.Range(0f,1f) > 0.7 && HasCompletedFirstTask) //Just for Debug for now!
        {
            AITask possibleTask = GetRandomObjectTask();

            if (possibleTask != null)
                return possibleTask;
        }

        if (CurrentGrid != null)
        {
            return GetRandomPositionTask();
        }

        return GetCentrePointTask();
    }

    private AITask CreateBasicGroup()
    {
        RaycastHit[] col = Physics.SphereCastAll(controller.Position, 10, Vector3.up);

        for (int i = 0; i < col.Length; i++)
        {
            if(col[i].collider.tag == "AI")
            {
                AIController ai = col[i].collider.gameObject.GetComponent<AIController>();

                if (!ai.Manager.currentTask.isInGroup)
                {
                    Group group = Instantiate(GameManager.instance.basicGroup, ai.Position, Quaternion.identity).GetComponent<Group>();
                    AITask groupTask = new AITask(group, true);
                    ai.Manager.OverrideCurrentTask(groupTask);
                    debugGroupLocations = (ai.Manager.controller.transform);
                    return groupTask;
                }
            }
        }

        return null;
    }

    private AITask FindGroup()
    {
        RaycastHit[] col = Physics.SphereCastAll(controller.Position, 10, Vector3.up);

        for (int i = 0; i < col.Length; i++)
        {
            if (col[i].collider.tag == "Group")
            {
                Group ai = col[i].collider.gameObject.GetComponent<Group>();
                return new AITask(ai);
            }
        }

        return null;
    }

    private AITask GetRandomObjectTask ()
    {
        Item possibleObject = HouseManager.GetRandomItem();

        if (possibleObject != null)
            return new AITask(possibleObject.controller);

        return null;
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
        tasksCompleted++;
        task.OnStart(Pawn, controller, () => StartTask(GenerateNewTask()));
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(currentTask.destination, Vector3.one);

        Gizmos.color = Color.cyan;
        if(debugGroupLocations)
            Gizmos.DrawWireCube(debugGroupLocations.position, Vector3.one * 2);
    }
}
