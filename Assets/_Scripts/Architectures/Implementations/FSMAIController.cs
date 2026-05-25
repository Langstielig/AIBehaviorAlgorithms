using UnityEngine;

public class FSMAIController : IAIController
{
    private NPCController npcController;
    private FSMState currentState;

    private EatFSMState eatState;
    private WorkFSMState workState;
    private SleepFSMState sleepState;
    private DropOffResourceFSMState dropOffResourceState;

    public void Initialize(NPCController npc)
    {
        npcController = npc;

        eatState = new EatFSMState(this, npcController);
        workState = new WorkFSMState(this, npcController);
        sleepState = new SleepFSMState(this, npcController);
        dropOffResourceState = new DropOffResourceFSMState(this, npcController);

        DecideNextState();
        //передать startState и начать его
    }

    public void Tick()
    {
        currentState.Tick();
    }

    public void ChangeState(FSMState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = newState;

        npcController.billboard.UpdateBestAction(currentState.StateName);

        currentState.Enter();
    }

    public void DecideNextState()
    {
        if(npcController.stats.energy <= 50)
        {
            ChangeState(sleepState);
        }
        else if(npcController.stats.hunger >= 75)
        {
            ChangeState(eatState);
        }
        else if(npcController.Inventory.HowFullIsStorage() == 1f)
        {
            ChangeState(dropOffResourceState);
        }
        else
        {
            ChangeState(workState);
        }
    }
}
