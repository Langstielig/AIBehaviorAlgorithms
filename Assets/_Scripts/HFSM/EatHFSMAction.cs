using UnityEngine;

public enum EatState
{
    FindFood,
    MoveToFood,
    Eat
}

public class EatHFSMAction : HFSMAction
{
    private EatState currentState;
    private Transform foodTarget;
    private bool finished;

    public override void Enter(NPCController npcController)
    {
        Debug.Log("HFSM Enter Eat");

        finished = false;
        currentState = EatState.FindFood;
    }

    public override void Exit(NPCController npcController)
    {
        Debug.Log("HFSM Exit Eat");

        foodTarget = null;
    }

    public override bool IsFinished()
    {
        return finished;
    }

    public override void Tick(NPCController npcController)
    {
        switch(currentState)
        {
            case EatState.FindFood:
                FindFood(npcController);
                break;
            case EatState.MoveToFood:
                MoveToFood(npcController);
                break;
            case EatState.Eat:
                Eat(npcController);
                break;
        }
    }

    private void FindFood(NPCController npcController)
    {
        Debug.Log("HFSM FindFood");

        foodTarget = npcController.context.FindNearestPosition(DestinationType.food, npcController.transform.position);
        if(foodTarget != null)
        {
            currentState = EatState.MoveToFood;
        }
    }

    private void MoveToFood(NPCController npcController)
    {
        Debug.Log("HFSM MoveToFood");

        if(foodTarget == null)
        {
            currentState = EatState.FindFood;
        }
        else
        {
            float distance = Vector3.Distance(npcController.transform.position, foodTarget.position);

            if(distance < npcController.minDistance)
            {
                currentState = EatState.Eat;
            }
            else
            {
                npcController.moveController.MoveTo(foodTarget.position);
            }
        }
    }

    private void Eat(NPCController npcController)
    {
        Debug.Log("HFSM Eat");

        npcController.DoEat();
        finished = true;
    }
}
