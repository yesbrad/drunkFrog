using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AITask
{
    public IInteractable interactable;
    public bool isComplete;

    private CharacterManager currentCharacter;

    [SerializeField] private System.Action onFinished;

    private AIController controller;

    public Vector3 destination;

    public bool isInGroup;

    public float time;

    public AITask (IInteractable newItem, bool isGroup = false) {
        interactable = newItem;
        destination = interactable.InteractPosition.position;
        isInGroup = isGroup;
    }

    public AITask(Vector3 positon)
    {
        destination = positon;
    }

    public virtual void OnStart(CharacterManager character, AIController iController, System.Action onFinish)
    {
        currentCharacter = character;
        onFinished = onFinish;
        controller = iController;
        controller.SetDestination(destination, OnDestinationReached, interactable);
        isComplete = false;
        time = Time.time;
    }

    public void OnDestinationReached ()
    {
        if (interactable != null)
        {
            interactable.StartInteract(currentCharacter, () => OnFinish());
        } 
        else
        {
            OnFinish();
        }
    }

    public void OnFinish ()
    {
        isComplete = true;

        if(onFinished != null)
            onFinished.Invoke();

        onFinished = null;
    }
}

public class AIManager : CharacterManager
{
    public AITask currentTask;

    [SerializeField]
    private int confusedNewTaskTime = 60;

    private AIController controller;
    private int tasksCompleted;

    public bool HasCompletedFirstTask { get { return tasksCompleted > 1; } }

    public override void Init(HouseManager initialHouse)
    {
        base.Init(initialHouse);
        controller = GetComponentInChildren<AIController>();
        StartAndGenerateTask();
    }

    public override void Update()
    {
        base.Update();

        if (currentTask.time + confusedNewTaskTime < Time.time)
        {
            StartAndGenerateTask();
        }
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
        currentTask.OnStart(this, controller, () => StartTask(GenerateNewTask()));
    }

    public AITask SelectTask ()
    {
        if (Random.Range(0f, 1f) < GameManager.instance.DesignBible.chanceOfSocialising && HasCompletedFirstTask)
        {
            AITask possibleTask = FindGroup();

            if (possibleTask != null)
            {
                return possibleTask;
            }
        }

        if (Random.Range(0f,1f) < GameManager.instance.DesignBible.chanceOfUsingRandomItem && HasCompletedFirstTask)
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
        task.OnStart(this, controller, () => StartTask(GenerateNewTask()));
    }
}
