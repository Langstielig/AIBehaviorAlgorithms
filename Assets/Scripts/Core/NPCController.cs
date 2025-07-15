using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    decide,
    move,
    execute
}

public class NPCController : MonoBehaviour
{
    [Header("Main Components")]
    public MoveController moveController { get; set; }
    public NPCInventory Inventory { get; set; }
    public Stats stats { get; set; }
    public Context context;
    public bool isUtilityAI;

    [Header ("Stats")]
    public float minDistance = 2f;
    public int sleepTime = 3;
    public int workTime = 3;

    [Header("Utility AI")]
    public AIBrain aiBrain { get; set; }
    public State currentState;

    [Header("Behavior Tree")]
    public BehaviorTree behaviorTree { get; set; }
    public bool isFinishedActing = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveController = GetComponent<MoveController>();
        Inventory = GetComponent<NPCInventory>();
        stats = GetComponent<Stats>();

        //UtilityAI
        if (isUtilityAI)
        {
            aiBrain = GetComponent<AIBrain>();
        }

        //Behavior Tree
        else
        {
            behaviorTree = GetComponent<BehaviorTree>();
        }

        if (!isUtilityAI)
        {


            ////Behavior Tree work
            //Node work = new Node("Work");
            //Leaf goToTree = new Leaf("Go to tree", moveController.MoveTo(Vector3.right));
            //Leaf chopTree = new Leaf("Chop the tree");

            //work.AddChild(goToTree);
            //work.AddChild(chopTree);
            //behaviorTree.AddChild(work);

            ////Behavior Tree eat
            //Node eat = new Node("Eat");

            //behaviorTree.AddChild(eat);

            ////Behavior Tree sleep
            //Node sleep = new Node("Sleep");
            //Node goToHome = new Node("Go to home");
            //Node sleeping = new Node("Sleeping");

            //sleep.AddChild(goToHome);
            //sleep.AddChild(sleeping);
            //behaviorTree.AddChild(sleep);

            ////Behavior Tree drop off recources
            //Node dropOffResources = new Node("Drop off resources");
            //Node goToStorage = new Node("Go to storage");
            //Node dropOff = new Node("Drop off");

            //dropOffResources.AddChild(goToStorage);
            //dropOffResources.AddChild(dropOff);
            //behaviorTree.AddChild(dropOffResources);

            //behaviorTree.PrintTree();
        }
    }

    void Update()
    {
        //if(aiBrain.finishedDeciding && isUtilityAI)
        //{
        //    aiBrain.finishedDeciding = false;
        //    aiBrain.bestAction.Execute(this);
        //}

        //stats.UpdateEnergy(AmIAtRestDestination());
        //stats.UpdateHunger();

        if (isUtilityAI)
        {
            FSMTick();
        } 
    }

    public void FSMTick()
    {
        if(currentState == State.decide)
        {
            aiBrain.DecideBestAction();

            if(Vector3.Distance(aiBrain.bestAction.RequiredDestination.position, transform.position) < 2f)
            {
                currentState = State.execute;
            }
            else
            {
                currentState = State.move;
            }
        }
        else if(currentState == State.move)
        {
            if(Vector3.Distance(aiBrain.bestAction.RequiredDestination.position, transform.position) < 2f)
            {
                currentState = State.execute;
            }
            else
            {
                moveController.MoveTo(aiBrain.bestAction.RequiredDestination.position);
            }
        }
        else if(currentState == State.execute)
        {
            if(aiBrain.finishedExecutingBestAction == false)
            {
                aiBrain.bestAction.Execute(this);
            }
            else
            {
                currentState = State.decide;
            }
        }
    }

    public bool AmIAtRestDestination()
    {
        return Vector3.Distance(transform.position, context.home.transform.position) <= context.MinDistance;
    }

    //public void onFinishedAction()
    //{
    //    aiBrain.DecideBestAction();
    //}

    #region MoveActions

    public Vector3 FindWorkPosition()
    {
        float minDistance = Mathf.Infinity;
        Transform nearestResource = null;

        List<Transform> resources = context.Destinations[DestinationType.resource];
        foreach (Transform resource in resources)
        {
            float distanceFromResource = Vector3.Distance(resource.position, transform.position);
            if (distanceFromResource < minDistance)
            {
                minDistance = distanceFromResource;
                nearestResource = resource;
            }
        }

        return nearestResource.position;
        //RequiredDestination = nearestResource;
        //npc.moveController.destination = RequiredDestination;
    }

    public Vector3 FindHomePosition()
    {
        Transform home = context.home.transform;
        return home.position;
    }

    public Vector3 FindStoragePosition()
    {
        Transform storage = context.storage.transform;
        return storage.position;
    }

    #endregion


    #region Coroutine

    public void DoWork()
    {
        isFinishedActing = false;
        StartCoroutine(WorkCoroutine(workTime));
    }

    IEnumerator WorkCoroutine(int time)
    {
        int counter = time;
        while(counter > 0)
        {
            yield return new WaitForSeconds(1);
            counter--;
        }

        //Debug.Log("I am working!");

        //Logic to update things involved with work
        Inventory.AddResource(ResourceType.wood, 10);

        //Decide our new best action after you finished this one
        //onFinishedAction();
        if (isUtilityAI)
        {
            aiBrain.finishedExecutingBestAction = true;
        }
        else
        {
            isFinishedActing = true;
        }
    }

    public void DoSleep()
    {
        if (!isFinishedActing)
        {
            StartCoroutine(SleepCoroutine(sleepTime));
        }
    }

    IEnumerator SleepCoroutine(int time)
    {
        int counter = time;
        while (counter > 0)
        {
            yield return new WaitForSeconds(1);
            counter--;
        }

        //Debug.Log("I slept and gained 1 energy!");

        //Logic to update energy
        stats.energy += 1;

        if (isUtilityAI)
        {
            aiBrain.finishedExecutingBestAction = true;
        }
        else
        {
            isFinishedActing = true;
        }
    }

    #endregion
}
