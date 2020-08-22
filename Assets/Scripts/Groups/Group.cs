using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : Interactable
{
    [System.Serializable]
    public class GroupCharacter
    {
        public AIManager aiManager;
        public System.Action onGroupLeave;

        public GroupCharacter (AIManager manager, System.Action onLeaveGroup)
        {
            aiManager = manager;
            onGroupLeave = onLeaveGroup;
            aiManager.Pawn.SetState(Pawn.PawnState.Talking);
        }
    }

    public List<GroupCharacter> characters = new List<GroupCharacter>();

    public float shrinkageTime = 5;
    [Range(2, 20)]
    public int groupLimit = 5;

    [Range(10, 100)]
    [Tooltip("Seconds")]
    public int maxTime = 60;

    private float currentBootTime;

    public float iceBreakTime = 5;
    private float currentIceBreakTime;
    bool isIceBroken;
    float maxTimeTimer;

    private void Awake()
    {
        currentBootTime = shrinkageTime;
        currentBootTime = iceBreakTime;
        maxTimeTimer = maxTime;
    }

    public override void Interact(Pawn pawn, System.Action onFinishInteraction)
    {
        //base.Interact(pawn, onFinishInteraction);
        if(characters.Count > groupLimit)
        {
            onFinishInteraction();
            return;
        }

        characters.Add(new GroupCharacter(pawn.GetComponentInParent<AIManager>(), onFinishInteraction));
    }

    private void Update()
    {
        currentBootTime -= Time.deltaTime;

        if(currentBootTime < 0)
        {
            if(characters.Count > 0)
                BootAI(characters[0].aiManager);

            currentBootTime = shrinkageTime;
        }

    }

    public void BootAllAI()
    {
        for (int i = 0; i < characters.Count; i++)
        {
            BootAI(characters[i].aiManager, 0);
        }
    }

    public void BootAI (AIManager manager, int index = 0)
    {
        manager.StartAndGenerateTask();
        manager.Pawn.SetState(Pawn.PawnState.Free);
        characters.RemoveAt(index);
    }

    //Doesnt Really Finish
    public override void EndTask()
    {
        base.EndTask();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 3);
    }
}
