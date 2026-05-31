public class HFSMAIController : IAIController
{
    private HFSMAction currentAction;
    private NPCController npcController;

    private SleepHFSMAction sleepHFSMAction;
    private EatHFSMAction eatHFSMAction;
    private DropOffResourceHFSMAction dropOffResourceHFSMAction;
    private WorkHFSMAction workHFSMAction;

    public void Initialize(NPCController npc)
    {
        npcController = npc;

        sleepHFSMAction = new SleepHFSMAction();
        eatHFSMAction = new EatHFSMAction();
        dropOffResourceHFSMAction = new DropOffResourceHFSMAction();
        workHFSMAction = new WorkHFSMAction();
    }

    public void Tick()
    {
        if(currentAction == null)
        {
            DecideAction();
        }

        currentAction.Tick(npcController);

        if(currentAction.IsFinished())
        {
            currentAction.Exit(npcController);
            currentAction = null;
        }
    }

    private void DecideAction()
    {
        if (npcController.stats.energy <= 50)
        {
            currentAction = sleepHFSMAction;
        }
        else if (npcController.stats.hunger >= 75)
        {
            currentAction = eatHFSMAction;
        }
        else if (npcController.Inventory.HowFullIsStorage() >= 1f)
        {
            currentAction = dropOffResourceHFSMAction;
        }
        else
        {
            currentAction = workHFSMAction;
        }

        currentAction.Enter(npcController);
    }
}
