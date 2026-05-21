public class SleepFSMState : FSMState
{
    private bool startedSleeping;

    public SleepFSMState(FSMAIController fsm, NPCController npcController)
    {
        this.fsm = fsm;
        this.npcController = npcController;
    }

    public override void Enter()
    {
        startedSleeping = false;
        target = npcController.context.FindNearestPosition(DestinationType.rest, npcController.transform.position);
    }

    public override void Tick()
    {
        if(!IsAtTarget())
        {
            MoveToTarget();
            return;
        }

        if(!startedSleeping)
        {
            startedSleeping = true;
            npcController.DoSleep();
        }

        if(npcController.isFinishedActing)
        {
            FinishState();
        }
    }

    public override void Exit()
    {
        target = null;
        startedSleeping = false;
    }
}
