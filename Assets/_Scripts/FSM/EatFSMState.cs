using UnityEngine;

public class EatFSMState : FSMState
{
    private bool startedEating;

    public EatFSMState(FSMAIController fsm, NPCController npcController)
    {
        this.fsm = fsm;
        this.npcController = npcController;
    }

    public override void Enter()
    {
        startedEating = false;
        target = npcController.context.FindNearestPosition(DestinationType.food, npcController.transform.position);
    }

    public override void Tick()
    {
        if(!IsAtTarget())
        {
            MoveToTarget();
            return;
        }

        if(!startedEating)
        {
            startedEating = true;
            npcController.DoEat();
        }

        if(npcController.isFinishedActing)
        {
            FinishState();
        }
    }

    public override void Exit()
    {
        startedEating = false;
        target = null;
    }
}
