using System.Collections;
using UnityEngine;

public enum State
{
    decide,
    //move,
    execute
}

public class NPCController : MonoBehaviour
{
    [Header("Main Components")]
    public MoveController moveController { get; set; }
    public NPCInventory Inventory { get; set; }
    public Stats stats { get; set; }
    public Context context;
    //public bool isUtilityAI;

    [Header ("Stats")]
    public float minDistance = 2f;
    public int sleepTime = 3;
    public int workTime = 3;
    public int eatTime = 2;
    public int dropOffResourcesTime = 1;

    [Header("AI")]
    public AIType aiType;
    [SerializeField] private IAIController aiController;

    [Header("Utility AI")]
    public AIBrain aiBrain { get; set; }
    public bool withPersonality;
    public Personality personality;
    public State currentState;
    public Transform currentTarget;

    [Header("HFSM")]
    public HFSMAction currentHFSMAction;

    [Header("Behavior Tree")]
    public BehaviorTree behaviorTree { get; set; }
    public bool isFinishedActing = false;
    public bool isActing = false;

    private void Start()
    {
        moveController = GetComponent<MoveController>();
        Inventory = GetComponent<NPCInventory>();
        stats = GetComponent<Stats>();

        //Debug.Log($"Время: {Time.time:F2}");

        InitializeAIController();

        ////UtilityAI
        //if (isUtilityAI)
        //{
        //    aiBrain = GetComponent<AIBrain>();
        //}
        ////Behavior Tree
        //else
        //{
        //    behaviorTree = GetComponent<BehaviorTree>();
        //}
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

        //if (isUtilityAI)
        //{
        //    FSMTick();
        //} 

        aiController.Tick();
    }

    private void InitializeAIController()
    {
        switch(aiType)
        {
            case AIType.UtilityAI:
                aiController = new UtilityAIController();
                aiBrain = GetComponent<AIBrain>();
                break;
            case AIType.BehaviorTree:
                aiController = new BehaviorTreeAIController();
                behaviorTree = GetComponent<BehaviorTree>();
                break;
            default:
                break;
        }

        aiController.Initialize(this);
    }

    public void FSMTick()
    {
        Debug.Log("Current state is " + currentState.ToString());
        //if(currentState == State.decide)
        //{
        //    aiBrain.DecideBestAction();

        //    if(Vector3.Distance(aiBrain.bestAction.RequiredDestination.position, transform.position) < 2f)
        //    {
        //        currentState = State.execute;
        //    }
        //    else
        //    {
        //        currentState = State.move;
        //    }
        //}
        //else if(currentState == State.move)
        //{
        //    if(Vector3.Distance(aiBrain.bestAction.RequiredDestination.position, transform.position) < 2f)
        //    {
        //        currentState = State.execute;
        //    }
        //    else
        //    {
        //        moveController.MoveTo(aiBrain.bestAction.RequiredDestination.position);
        //        //moveController.MoveTo(currentTarget.position);
        //    }
        //}
        //else if(currentState == State.execute)
        //{
        //    if(aiBrain.finishedExecutingBestAction == false)
        //    {
        //        aiBrain.bestAction.Execute(this);
        //    }
        //    else
        //    {
        //        currentState = State.decide;
        //    }
        //}

        if(currentState == State.decide)
        {
            aiBrain.DecideBestAction();
            currentState = State.execute;
        }
        else if(currentState == State.execute)
        {
            if(currentHFSMAction == null)
            {
                //currentHFSMAction = new EatHFSMAction();
                //currentHFSMAction = new SleepHFSMAction();
                currentHFSMAction = aiBrain.bestAction.CreateHFSMAction();
                currentHFSMAction.Enter(this);
            }

            currentHFSMAction.Tick(this);

            if(currentHFSMAction.IsFinished())
            {
                currentHFSMAction.Exit(this);
                currentHFSMAction = null;
                FinishExecutingBestAction();
                currentState = State.decide;
            }
        }
    }

    public void FinishExecutingBestAction()
    {
        if (aiType == AIType.UtilityAI)
        {
            aiBrain.finishedExecutingBestAction = true;
        }
    }

    //public bool AmIAtRestDestination()
    //{
    //    return Vector3.Distance(transform.position, context.home.transform.position) <= context.MinDistance;
    //}

    //public void onFinishedAction()
    //{
    //    aiBrain.DecideBestAction();
    //}

    #region MoveActions

    public void FindWorkPosition()
    {
        //Transform workTransform = context.FindNearestPosition(DestinationType.resource, transform.position);
        //return workTransform.position;

        //currentTarget = context.FindNearestPosition(DestinationType.resource, transform.position);

        //float minDistance = Mathf.Infinity;
        //Transform nearestResource = null;
        //List<Transform> resources = context.Destinations[DestinationType.resource];
        //foreach (Transform resource in resources)
        //{
        //    float distanceFromResource = Vector3.Distance(resource.position, transform.position);
        //    if (distanceFromResource < minDistance)
        //    {
        //        minDistance = distanceFromResource;
        //        nearestResource = resource;
        //    }
        //}

        //return nearestResource.position;
        //RequiredDestination = nearestResource;
        //npc.moveController.destination = RequiredDestination;
    }

    public void FindFoodPosition()
    {
        //Transform foodTransform = context.FindNearestPosition(DestinationType.food, transform.position);
        //return foodTransform.position;
        //currentTarget = context.FindNearestPosition(DestinationType.food, transform.position);
    }

    public void FindHomePosition()
    {
        //Transform home = context.home.transform;
        //return home.position;

        //currentTarget = context.FindNearestPosition(DestinationType.rest, transform.position);
    }

    public void FindStoragePosition()
    {
        //Transform storage = context.storage.transform;
        //return storage.position;

        //currentTarget = context.FindNearestPosition(DestinationType.storage, transform.position);
    }

    #endregion


    #region Coroutine

    public void DoWork()
    {
        //isFinishedActing = false;
        //StartCoroutine(WorkCoroutine(workTime));

        if(!isActing)
        {
            isActing = true;
            isFinishedActing = false;
            StartCoroutine(WorkCoroutine(workTime));
        }
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
        // TODO: Maybe wrong logic too
        Inventory.AddResource(ResourceType.wood, 10);

        //Decide our new best action after you finished this one
        //onFinishedAction();
        if (aiType == AIType.UtilityAI)
        {
            FinishExecutingBestAction();
        }
        else
        {
            isFinishedActing = true;
        }

        isActing = false;
    }

    public void DoSleep()
    {
        //if (!isFinishedActing)
        //{
        //    StartCoroutine(SleepCoroutine(sleepTime));
        //}

        if(!isActing)
        {
            isActing = true;
            isFinishedActing = false;
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
        stats.energy += 20;

        if (aiType == AIType.UtilityAI)
        {
            FinishExecutingBestAction();
        }
        else
        {
            isFinishedActing = true;
        }

        isActing = false;
    }

    public void DoDropOffResources()
    {
        //if (!isFinishedActing)
        //{
        //    StartCoroutine(DropOffResourcesCoroutine(dropOffResourcesTime));
        //}

        if(!isActing)
        {
            isActing = true;
            isFinishedActing = false;
            StartCoroutine(DropOffResourcesCoroutine(dropOffResourcesTime));
        }
    }

    IEnumerator DropOffResourcesCoroutine(int time)
    {
        int counter = time;
        while (counter > 0)
        {
            yield return new WaitForSeconds(1);
            counter--;
        }

        //Debug.Log("I slept and gained 1 energy!");

        //Logic to update 
        //TODO: wring logic of adding money
        Inventory.RemoveAllResource();
        stats.money += 20;

        if (aiType == AIType.UtilityAI)
        {
            FinishExecutingBestAction();
        }
        else
        {
            isFinishedActing = true;
        }

        isActing = false;
    }

    public void DoEat()
    {
        //if (!isFinishedActing)
        //{
        //    StartCoroutine(EatCoroutine(eatTime));
        //}
        if(!isActing)
        {
            isActing = true;
            isFinishedActing = false;
            StartCoroutine(EatCoroutine(eatTime));
        }
    }

    IEnumerator EatCoroutine(int time)
    {
        int counter = time;
        while (counter > 0)
        {
            yield return new WaitForSeconds(1);
            counter--;
        }

        //Debug.Log("I slept and gained 1 energy!");

        //Logic to update hunger
        //Inventory.RemoveAllResource();
        //stats.money += 20;

        //TODO: Wrong logic of update hunger and money
        stats.hunger -= 30;
        stats.money -= 10;

        if (aiType == AIType.UtilityAI)
        {
            FinishExecutingBestAction();
        }
        else
        {
            isFinishedActing = true;
        }

        isActing = false;
    }

    #endregion
}
