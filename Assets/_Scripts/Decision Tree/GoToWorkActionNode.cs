using UnityEngine;

public class GoToWorkActionNode : ActionNode
{
    public GoToWorkActionNode(
        NPCController npcController)
        : base(npcController)
    {

    }

    public override string StateName => "Work";

    public override void Execute()
    {
        Transform resource =
            npcController.context.FindNearestPosition(
                DestinationType.resource,
                npcController.transform.position);

        if (resource != null)
        {
            npcController.currentTarget = resource;

            npcController.moveController
                .MoveTo(resource.position);
        }
    }
}
