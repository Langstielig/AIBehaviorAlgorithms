using UnityEngine;

public enum DropOffResourceState
{
    FindStorage,
    MoveToStorage,
    DropOffResource
}

public class DropOffResourceHFSMAction : HFSMAction
{
    private DropOffResourceState currentState;
    private Transform storage;
    private bool finished;

    public override void Enter(NPCController npcController)
    {
        Debug.Log("HFSM Enter DropOffResource");

        finished = false;
        currentState = DropOffResourceState.FindStorage;
    }

    public override void Exit(NPCController npcController)
    {
        Debug.Log("HFSSM Exit DropOffResource");

        storage = null;
    }

    public override bool IsFinished()
    {
        return finished;
    }

    public override void Tick(NPCController npcController)
    {
        switch(currentState)
        {
            case DropOffResourceState.FindStorage:
                FindStorage(npcController);
                break;
            case DropOffResourceState.MoveToStorage:
                MoveToStorage(npcController);
                break;
            case DropOffResourceState.DropOffResource:
                DropOffResource(npcController);
                break;
        }
    }

    private void FindStorage(NPCController npcController)
    {
        Debug.Log("HFSM FindSStorage");

        storage = npcController.context.FindNearestPosition(DestinationType.storage, npcController.transform.position);
        if (storage != null)
        {
            currentState = DropOffResourceState.MoveToStorage;
        }
    }

    private void MoveToStorage(NPCController npcController)
    {
        Debug.Log("HFSM MoveToStorage");

        if (storage == null)
        {
            currentState = DropOffResourceState.FindStorage;
        }
        else
        {
            float distance = Vector3.Distance(npcController.transform.position, storage.position);

            if (distance < npcController.minDistance)
            {
                currentState = DropOffResourceState.DropOffResource;
            }
            else
            {
                npcController.moveController.MoveTo(storage.position);
            }
        }
    }

    private void DropOffResource(NPCController npcController)
    {
        Debug.Log("HFSM DropOffResource");

        npcController.DoDropOffResources();
        finished = true;
    }
}
