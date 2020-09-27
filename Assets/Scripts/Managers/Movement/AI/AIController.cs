using System;
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
        public bool hasInteractable;
        
        public Vector3 jobDestination;
        public IInteractable jobInteractable;
        public bool useInteractable;

        private float completeTime;
        private float currentTime;

        private Action onSuccess, onFailed;

        private NavMeshPath path = new NavMeshPath();

        public Job (NavMeshAgent jobAgent, Vector3 destination, Action onSuccess, Action onFailed)
        {
            navAgent = jobAgent;
            jobDestination = destination;
            this.onSuccess = onSuccess;
            this.onFailed = onFailed;
        }

        public Job(NavMeshAgent jobAgent, IInteractable interactable, Action onSuccess, Action onFailed)
        {
            navAgent = jobAgent;
            this.onSuccess = onSuccess;
            this.onFailed = onFailed;
            jobInteractable = interactable;
            useInteractable = true;
        }

        public void Init ()
        {
            inTransit = true;

            Vector3 destination = useInteractable ? jobInteractable.InteractPosition.position : jobDestination;
            navAgent.CalculatePath(destination, path);
            navAgent.SetPath(path);

            currentTime = 0;

            float totalPathDistance = 0;
            Vector3[] corners = path.corners;

            for (int i = 0; i < corners.Length; i++)
            {
                totalPathDistance += Vector3.Distance(corners[i], corners[Mathf.Clamp(i + 1, 0, corners.Length - 1)]);
            }

            completeTime = totalPathDistance / navAgent.speed + GameManager.instance.DesignBible.aiStuckBuffer;


            //Debug.LogError("Complete Time: " + completeTime);


            if (!IsGoodPath())
            {
                onFailed.Invoke();
                return;
            }

            if (useInteractable)
                navAgent.avoidancePriority = 10;

        }

        public bool IsGoodPath() => navAgent.pathStatus == NavMeshPathStatus.PathComplete;

        public void CheckDestination ()
        {
            // Finish the job when its reached its destination
            if (inTransit && !navAgent.pathPending && navAgent.hasPath)
            {
                currentTime += Time.deltaTime;
                    
                if (navAgent.remainingDistance <= navAgent.stoppingDistance)
                {
                    Debug.Log("Reached Destination", navAgent.gameObject);
                    CompleteJob();
                    return;                  
                }

                // This is here to see if we can not reach out next target;
                if(currentTime > completeTime)
                {
                    Debug.Log("Failed to reach next Point", navAgent.gameObject);
                    FailJob();
                    return;
                }

                
                if(!IsGoodPath())
                {
                    Debug.LogWarning("path wasnt clear finding new task", navAgent.gameObject);
                    FailJob();
                    return;
                }
            }
        }

        public void CompleteJob()
        {
            inTransit = false;
            onSuccess.Invoke();
            onSuccess = null;
            navAgent.avoidancePriority = 60;
        }

        public void FailJob ()
        {
            inTransit = false;
            onFailed.Invoke();
            onFailed = null;
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
        agent.autoRepath = false;
    }

    private void OnDrawGizmos()
    {
        if (agent.hasPath)
        {
            for (int i = 0; i < agent.path.corners.Length; i++)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawCube(agent.path.corners[i], Vector3.one);
            }
        }
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

    public void StartJob (Vector3 position, Action onSuccess, Action onFailed)
    {
        currentJob = new Job(agent, position, onSuccess, onFailed);
        currentJob.Init();
    }
    public void StartJob(IInteractable interactable, Action onSuccess, Action onFailed)
    {
        currentJob = new Job(agent, interactable, onSuccess, onFailed);
        currentJob.Init();
    }


}
