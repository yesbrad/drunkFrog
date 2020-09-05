using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : MonoBehaviour, IInteractable
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

    [SerializeField]
    private Transform interactPosition;

    public Transform InteractPosition { get { return interactPosition; } }

    private float currentBootTime;

    public bool occupied { get; set; }

    public System.Action onTaskFinished { get; set; }

    private void Awake()
    {
        currentBootTime = shrinkageTime;
    }

    public void Interact(CharacterManager manager, System.Action onFinishInteraction)
    { 
        if(characters.Count > groupLimit)
        {
            onFinishInteraction();
            return;
        }
        characters.Add(new GroupCharacter(manager.GetComponentInParent<AIManager>(), onFinishInteraction));
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 3);
    }

    
}
