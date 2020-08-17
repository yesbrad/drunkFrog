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
        public Interactable parentInteractable;

        public Job (NavMeshAgent jobAgent, Vector3 destination, System.Action desinationReached, Interactable interactable)
        {
            navAgent = jobAgent;
            OnReachDestination = desinationReached;
            jobDestination = destination;
            parentInteractable = interactable;
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
                    }
                }
            }

            /*/ Cancel the Job if the parent is Being Taken;
            if (parentInteractable.occupied)
            {
                inTransit = false;
                OnReachDestination.Invoke();
                OnReachDestination = null;
            }
            */
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

    public void SetDestination (Vector3 destination, System.Action desinationReached, Interactable interactable)
    {
        currentJob = new Job(agent, destination, desinationReached, interactable);
        currentJob.Init();
    }

}
