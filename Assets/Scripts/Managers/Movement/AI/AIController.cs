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
        public IInteractable parentInteractable;
        public bool hasInteractable;
        
        public Vector3 jobDestination;
        public Transform jobDestinationTransform;
        public bool useTransform;

        public Job (NavMeshAgent jobAgent, Vector3 destination, System.Action desinationReached, IInteractable interactable)
        {
            navAgent = jobAgent;
            OnReachDestination = desinationReached;
            jobDestination = destination;
            parentInteractable = interactable;
        }

        public Job(NavMeshAgent jobAgent, Transform destination, System.Action desinationReached, IInteractable interactable)
        {
            navAgent = jobAgent;
            OnReachDestination = desinationReached;
            jobDestinationTransform = destination;
            parentInteractable = interactable;
            useTransform = true;
        }

        public void Init ()
        {
            inTransit = true;
            navAgent.destination = useTransform ? jobDestinationTransform.position : jobDestination;
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

            if (useTransform)
            {
                if(jobDestinationTransform == null)
                {
                    inTransit = false;
                    OnReachDestination.Invoke();
                    OnReachDestination = null;
                }
            }
        }
    }

    private NavMeshAgent agent;
    public AIManager Manager { get; private set; }

    public Job currentJob;

    public override void Init()
    {
        base.Init();
        Manager = GetComponentInParent<AIManager>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if(currentJob != null)
        {
            currentJob.CheckDestination();
        }

        SetVelocity(agent.velocity.sqrMagnitude);
    }

    public override void SetRotation(Vector3 rotation)
    {
        base.SetRotation(rotation);
        transform.rotation = Quaternion.Euler(rotation);
    }

    public void SetDestination (Vector3 destination, System.Action desinationReached, IInteractable interactable)
    {
        currentJob = new Job(agent, destination, desinationReached, interactable);
        currentJob.Init();
    }

}
