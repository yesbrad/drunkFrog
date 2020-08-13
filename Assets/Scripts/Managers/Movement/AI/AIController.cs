using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIController : Pawn
{
    private NavMeshAgent agent;
    public AIManager Manager { get; private set; }

    private void Awake()
    {
        Manager = GetComponentInParent<AIManager>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        Debug.Log(Manager.HouseManager);
    }

    private void Update()
    {
        if(Manager.HouseManager.houseOwner != null)
        {
            SetDestination(Manager.HouseManager.houseOwner.Pawn.Position);
        }
        else
        {
            SetDestination(GameManager.instance.houseManagers[0].houseOwner.Pawn.Position); // Theres always one player
        }
    }

    public void SetDestination (Vector3 destination)
    {
        agent.destination = destination;
    }

}
