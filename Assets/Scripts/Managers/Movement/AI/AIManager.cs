using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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

    public bool isPosition;

    public AITask (IInteractable newItem) {
        interactable = newItem;
        destination = interactable.InteractPosition.position;
    }

    public AITask(Vector3 positon)
    {
        destination = positon;
        isPosition = true;
    }

    public void OnStart(CharacterManager character, AIController iController, System.Action onFinish)
    {
        currentCharacter = character;
        onFinished = onFinish;
        controller = iController;

        if(isPosition)
            controller.StartJob(destination, OnDestinationReached);
        else
            controller.StartJob(interactable, OnDestinationReached);

        isComplete = false;
        time = Time.time;
    }

    public void OnDestinationReached ()
    {
        if (interactable != null && interactable.IsFull() == false)
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
    private Text debugTaskText;

    private AIController controller;
    private int tasksCompleted;

    public bool HasCompletedFirstTask { get { return tasksCompleted > 1; } }

    public AIStats Stats { get; private set; }
    public AIClass AIClass { get; private set; }

    public void Init(HouseManager initialHouse, AIClass aIClass)
    {
        base.Init(initialHouse);
        controller = GetComponentInChildren<AIController>();
        Stats = new AIStats(aIClass);
        AIClass = aIClass;
        StartAndGenerateTask();
    }

    public override void Update()
    {
        base.Update();

        if (Stats != null)
            Stats.TickStats();

        /* Shouldnt be doinbg this time to fix dat AI
        if (currentTask.time + GameManager.instance.designBible.maxAITaskTime < Time.time)
        {
            if(currentTask.interactable != null)
                currentTask.interactable.occupants = false;
            
            StartAndGenerateTask();
        }
        */

        if (debugTaskText != null)
        {
            debugTaskText.text = $"{Stats.GetStat(AIStatTypes.Soberness).amount}";
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

    private int lastSelection = -1;

    private bool WasLastSelected(int current) => lastSelection != current;

    public AITask SelectTask ()
    {
        if (Stats.GetStatAmount(AIStatTypes.Soberness) > AIClass.obtainingAlcoholThreshold && WasLastSelected(0) && HasCompletedFirstTask)
        {
            AITask possibleTask = GetStatObjectTask(AIStatTypes.Soberness);

            if (possibleTask != null)
            {
                lastSelection = 0;
                return possibleTask;
            }
        }

        if (Stats.GetStatAmount(AIStatTypes.Thirst) > AIClass.obtainingWaterThreshold && WasLastSelected(1) && HasCompletedFirstTask)
        {
            AITask possibleTask = GetStatObjectTask(AIStatTypes.Thirst);

            if (possibleTask != null)
            {
                lastSelection = 1;
                return possibleTask;
            }
        }

        if (Stats.GetStatAmount(AIStatTypes.Hunger) > AIClass.obtainingFoodThreshold && WasLastSelected(2) && HasCompletedFirstTask)
        {
            AITask possibleTask = GetStatObjectTask(AIStatTypes.Hunger);

            if (possibleTask != null)
            {
                lastSelection = 2;
                return possibleTask;
            }
        }

        if (GetOdds() < AIClass.obtainingSocializingThreshold && WasLastSelected(3) && HasCompletedFirstTask)
        {
            AITask possibleTask = FindGroup();

            if (possibleTask != null)
            {
                lastSelection = 3;
                return possibleTask;
            }
        }

        // Do somthing fun if theres anyhting remotyly fun in the house
        if (HasCompletedFirstTask)
        {
            AITask possibleTask = GetStatObjectTask(AIStatTypes.Boardness);

            if (possibleTask != null)
                return possibleTask;
        }

        if (CurrentGrid != null)
        {
            return GetRandomPositionTask();
        }

        return GetCentrePointTask();
    }

    private static float GetOdds()
    {
        return Random.Range(0f, 1f);
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
        try
        {
            ItemController possibleObject = HouseManager.HouseInventory.FindRandomFunItem();

            if (possibleObject != null)
                return new AITask(possibleObject);

            return null;
        }
        catch
        {
            return null;
        }
    }

    private AITask GetStatObjectTask(AIStatTypes type)
    {
        try
        {
            ItemController possibleObject = HouseManager.HouseInventory.FindItem(type);

            if (possibleObject != null)
                return new AITask(possibleObject);

            return null;
        }
        catch
        {
            return null;
        }
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
