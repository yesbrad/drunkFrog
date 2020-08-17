using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : Interactable
{
    [System.Serializable]
    public class GroupCharacter
    {
        public Pawn Pawn;
        public System.Action onGroupLeave;

        public GroupCharacter (Pawn newPawn, System.Action onLeaveGroup)
        {
            Pawn = newPawn;
            onGroupLeave = onLeaveGroup;
        }
    }

    public List<GroupCharacter> characters = new List<GroupCharacter>();

    public float shrinkageTime = 5;

    private float currentBootTime;

    private void Awake()
    {
        currentBootTime = shrinkageTime;
    }

    public override void Interact(Pawn pawn, System.Action onFinishInteraction)
    {
        //base.Interact(pawn, onFinishInteraction);
        characters.Add(new GroupCharacter(pawn, onFinishInteraction));
    }

    private void Update()
    {
        // Will Never let a Group Die at the moment
        if(characters.Count > 2)
        {
            currentBootTime -= Time.deltaTime;

            if(currentBootTime < 0)
            {
                characters[0].Pawn.GetComponentInParent<AIManager>().StartAndGenerateTask();
                characters.RemoveAt(0);
                Debug.Log("Bootong");
                currentBootTime = shrinkageTime;
            }
        }
    }

    //Doesnt Really Finish
    public override void EndTask()
    {
        base.EndTask();
    }
}
