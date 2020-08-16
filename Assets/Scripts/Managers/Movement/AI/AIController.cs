using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIController : Pawn
{
    [System.Serializable]
    public class Job
    {
        public NavMeshAgent navAgent;

        public bool inTransit;
        public System.Action OnReachDestination;
        public Vector3 jobDestination;

        public Job (NavMeshAgent jobAgent, Vector3 destination, System.Action desinationReached)
        {
            navAgent = jobAgent;
            OnReachDestination = desinationReached;
            jobDestination = destination;
        }

        public void Init ()
        {
            inTransit = true;
            navAgent.destination = jobDestination;
            //Debug.Log($"JobDestination: {jobDestination} || current destination: {navAgent.destination}");
        }

        public void CheckDestination ()
        {
            if (!navAgent.pathPending && inTransit)
            {
                if (navAgent.remainingDistance <= navAgent.stoppingDistance)
                {
                    if (!navAgent.hasPath || navAgent.velocity.sqrMagnitude == 0f)
                    {
                        inTransit = false;
                        OnReachDestination.Invoke();
                        OnReachDestination = null;
                        Debug.Log("Made It to dest");
                    }
                }
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
        currentJob.Init();
    }

}
