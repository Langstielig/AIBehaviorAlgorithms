public class DropOffResourceFSMState : FSMState
{
    private bool startedDropOffRecource;

    public override string StateName => "Drop off resources";

    public DropOffResourceFSMState(FSMAIController fsm, NPCController npcController)
    {
        this.fsm = fsm;
        this.npcController = npcController;
    }

    public override void Enter()
    {
        startedDropOffRecource = false;
        target = npcController.context.FindNearestPosition(DestinationType.storage, npcController.transform.position);
    }

    public override void Tick()
    {
        if(!IsAtTarget())
        {
            MoveToTarget();
            return;
        }

        if(!startedDropOffRecource)
        {
            startedDropOffRecource = true;
            npcController.DoDropOffResources();
        }

        if(npcController.isFinishedActing)
        {
            FinishState();
        }
    }

    public override void Exit()
    {
        target = null;
        startedDropOffRecource = false;
    }
}
