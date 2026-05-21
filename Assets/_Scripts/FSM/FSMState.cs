using UnityEngine;

public abstract class FSMState
{
    protected FSMAIController fsm;
    protected NPCController npcController;
    protected Transform target;

    protected bool IsAtTarget()
    {
        if (target == null)
        {
            return false;
        }

        return Vector3.Distance(npcController.transform.position, target.position) <= npcController.minDistance;
    }

    protected void MoveToTarget()
    {
        if (target != null)
        {
            npcController.moveController.MoveTo(target.position);
        }
    }

    protected void FinishState()
    {
        npcController.isFinishedActing = false;
        fsm.DecideNextState();
    }

    public virtual void Enter() { }

    public virtual void Tick() { }

    public virtual void Exit() { }
}
