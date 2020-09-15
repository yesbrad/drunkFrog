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
        public bool hasInteractable;
        
        public Vector3 jobDestination;
        public IInteractable jobInteractable;
        public bool useTransform;

        public Job (NavMeshAgent jobAgent, Vector3 destination, System.Action desinationReached)
        {
            navAgent = jobAgent;
            OnReachDestination = desinationReached;
            jobDestination = destination;
        }

        public Job(NavMeshAgent jobAgent, IInteractable interactable, System.Action desinationReached)
        {
            navAgent = jobAgent;
            OnReachDestination = desinationReached;
            jobInteractable = interactable;
            useTransform = true;
        }

        public void Init ()
        {
            inTransit = true;
            navAgent.destination = useTransform ? jobInteractable.InteractPosition.position : jobDestination;

            if (useTransform)
            {
                navAgent.avoidancePriority = 10;
            }
        }

        public void CheckDestination ()
        {
            // Finish the job when its reached its destination
            if (!navAgent.pathPending && inTransit)
            {
                if (navAgent.remainingDistance <= navAgent.stoppingDistance)
                {
                    if (!navAgent.hasPath || navAgent.velocity.sqrMagnitude == 0f)
                    {
                        FinishJob();
                    }
                }
            }

            
            // If the current interactable is currenly being used Finish the job
            //if(inTransit && (jobInteractable != null) && (jobInteractable.IsFull() || !jobInteractable.InHand))
            //{
             //   FinishJob();
            //}
            

            if (useTransform)
            {
                if(jobInteractable == null)
                {
                    FinishJob();
                }
            }
        }

        public void FinishJob()
        {
            inTransit = false;
            OnReachDestination.Invoke();
            OnReachDestination = null;
            navAgent.avoidancePriority = 60;
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

    public void StartJob (Vector3 position, System.Action desinationReached)
    {
        currentJob = new Job(agent, position, desinationReached);
        currentJob.Init();
    }
    public void StartJob(IInteractable interactable, System.Action desinationReached )
    {
        currentJob = new Job(agent, interactable, desinationReached);
        currentJob.Init();
    }


}
