using UnityEngine;

public class WorkFSMState : FSMState
{
    private bool startedWorking;

    public override string StateName => "Work";

    public WorkFSMState(FSMAIController fsm, NPCController npcController)
    {
        this.fsm = fsm;
        this.npcController = npcController;
    }

    public override void Enter()
    {
        startedWorking = false;
        target = npcController.context.FindNearestPosition(DestinationType.resource, npcController.transform.position);
        //npcController.FindWorkPosition();
    }

    public override void Tick()
    {
        if(!IsAtTarget())
        {
            MoveToTarget();
            return;
        }

        if(!startedWorking)
        {
            startedWorking = true;
            npcController.DoWork();
        }

        if(npcController.isFinishedActing)
        {
            FinishState();
        }
    }

    public override void Exit()
    {
        startedWorking = false;
        target = null;
    }
}
