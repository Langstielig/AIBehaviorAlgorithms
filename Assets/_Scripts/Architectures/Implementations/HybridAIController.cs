public class HybridAIController : IAIController
{
    private NPCController npcController;

    private HFSMAction currentAction;

    private SleepHFSMAction sleepAction;
    private EatHFSMAction eatAction;
    private WorkHFSMAction workAction;
    private DropOffResourceHFSMAction dropOffResourceAction;

    public void Initialize(NPCController npc)
    {
        npcController = npc;
        npcController.currentState = State.decide;

        sleepAction = new SleepHFSMAction();
        eatAction = new EatHFSMAction();
        workAction = new WorkHFSMAction();
        dropOffResourceAction = new DropOffResourceHFSMAction();
    }

    public void Tick()
    {
        //npcController.HybridTick();

        if (currentAction == null)
        {
            ChooseAction();
        }

        currentAction.Tick(npcController);

        if(currentAction.IsFinished())
        {
            currentAction.Exit(npcController);
            currentAction = null;
            npcController.FinishExecutingBestAction();
        }
    }

    private void ChooseAction()
    {
        npcController.aiBrain.DecideBestAction();
        Action bestAction = npcController.aiBrain.bestAction;

        if (bestAction is Sleep)
        {
            currentAction = sleepAction;
        }
        else if (bestAction is Eat)
        {
            currentAction = eatAction;
        }
        else if (bestAction is Work)
        {
            currentAction = workAction;
        }
        else
        {
            currentAction = dropOffResourceAction;
        }

        currentAction.Enter(npcController);
    }
}
