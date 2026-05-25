using UnityEngine;

public enum WorkState
{
    FindTree,
    MoveToTree,
    Work
}

public class WorkHFSMAction : HFSMAction
{
    private WorkState currentState;
    private Transform tree;
    private bool finished;

    public override string StateName => "Work";

    public override void Enter(NPCController npcController)
    {
        Debug.Log("HFSM Enter Work");

        npcController.billboard.UpdateBestAction(StateName);

        finished = false;
        currentState = WorkState.FindTree;
    }

    public override void Exit(NPCController npcController)
    {
        Debug.Log("HFSM Exit Work");

        tree = null;
    }

    public override bool IsFinished()
    {
        return finished;
    }

    public override void Tick(NPCController npcController)
    {
        switch(currentState)
        {
            case WorkState.FindTree:
                FindTree(npcController);
                break;
            case WorkState.MoveToTree:
                MoveToTree(npcController);
                break;
            case WorkState.Work:
                Work(npcController);
                break;
        }
    }

    private void FindTree(NPCController npcController)
    {
        Debug.Log("HFSM FindTree");

        tree = npcController.context.FindNearestPosition(DestinationType.resource, npcController.transform.position);
        if(tree != null)
        {
            currentState = WorkState.MoveToTree;
        }
    }

    private void MoveToTree(NPCController npcController)
    {
        Debug.Log("HFSM MoveToTree");

        if (tree == null)
        {
            currentState = WorkState.FindTree;
        }
        else
        {
            float distance = Vector3.Distance(npcController.transform.position, tree.position);

            if (distance < npcController.minDistance)
            {
                currentState = WorkState.Work;
            }
            else
            {
                npcController.moveController.MoveTo(tree.position);
            }
        }
    }

    private void Work(NPCController npcController)
    {
        Debug.Log("HFSM Work");

        npcController.DoWork();
        finished = true;
    }
}
