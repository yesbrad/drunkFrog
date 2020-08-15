using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIController : Pawn
{
    public class Job
    {
        public NavMeshAgent navAgent;

        public bool inTransit;
        public System.Action OnReachDestination;

        public Job (NavMeshAgent jobAgent, Vector3 destination, System.Action desinationReached)
        {
            navAgent = jobAgent;
            inTransit = true;
            OnReachDestination = desinationReached;
            navAgent.destination = destination;
        }

        public void CheckDestination ()
        {
            if (navAgent.remainingDistance <= navAgent.stoppingDistance && inTransit)
            {
                
                OnReachDestination.Invoke();
                OnReachDestination = null;

                inTransit = false;
            }
        }
    }

    private NavMeshAgent agent;
    public AIManager Manager { get; private set; }

    public Job currentJob;

    private void Awake()
    {
        Manager = GetComponentInParent<AIManager>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
    }

    private void Update()
    {
        if(currentJob != null)
        {
            currentJob.CheckDestination();
        }
    }

    public void SetDestination (Vector3 destination, System.Action desinationReached)
    {
        currentJob = new Job(agent, destination, desinationReached);
    }

}
