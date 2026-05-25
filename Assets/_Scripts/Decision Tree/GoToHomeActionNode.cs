using UnityEngine;

public class GoToHomeActionNode : ActionNode
{
    public GoToHomeActionNode(
        NPCController npcController)
        : base(npcController)
    {

    }

    public override string StateName => "Sleep";

    public override void Execute()
    {
        Transform home =
            npcController.context.FindNearestPosition(
                DestinationType.rest,
                npcController.transform.position);

        if (home != null)
        {
            npcController.currentTarget = home;

            npcController.moveController
                .MoveTo(home.position);
        }
    }
}
