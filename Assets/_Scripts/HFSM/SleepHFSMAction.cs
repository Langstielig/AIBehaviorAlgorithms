using UnityEngine;

public enum SleepState
{
    FindHome,
    MoveToHome,
    Sleep
}

public class SleepHFSMAction : HFSMAction
{
    private SleepState currentState;
    private Transform home;
    private bool finished;

    public override string StateName => "Sleep";

    public override void Enter(NPCController npcController)
    {
        Debug.Log("HFSM Enter Sleep");

        npcController.billboard.UpdateBestAction(StateName);

        finished = false;
        currentState = SleepState.FindHome;
    }

    public override void Exit(NPCController npcController)
    {
        Debug.Log("HFSM Exit Sleep");
        home = null;
    }

    public override bool IsFinished()
    {
        return finished;
    }

    public override void Tick(NPCController npcController)
    {
        switch(currentState)
        {
            case SleepState.FindHome:
                FindHome(npcController);
                break;
            case SleepState.MoveToHome:
                MoveToHome(npcController);
                break;
            case SleepState.Sleep:
                Sleep(npcController);
                break;
        }
    }

    private void FindHome(NPCController npcController)
    {
        Debug.Log("HFSM FindHome");

        home = npcController.context.FindNearestPosition(DestinationType.rest, npcController.transform.position);
        if (home != null)
        {
            currentState = SleepState.MoveToHome;
        }
    }

    private void MoveToHome(NPCController npcController)
    {
        Debug.Log("HFSM MoveToHome");

        if(home == null)
        {
            currentState = SleepState.FindHome;
        }
        else
        {
            float distance = Vector3.Distance(npcController.transform.position, home.position);

            if(distance < npcController.minDistance)
            {
                currentState = SleepState.Sleep;
            }
            else
            {
                npcController.moveController.MoveTo(home.position);
            }
        }
    }

    private void Sleep(NPCController npcController)
    {
        Debug.Log("HFSM Sleep");

        npcController.DoSleep();
        finished = true;
    }
}
