using UnityEngine;

public class GoToStorageActionNode : ActionNode
{
    public GoToStorageActionNode(
        NPCController npcController)
        : base(npcController)
    {

    }

    public override string StateName => "Drop off resources";

    public override void Execute()
    {
        Transform storage =
            npcController.context.FindNearestPosition(
                DestinationType.storage,
                npcController.transform.position);

        if (storage != null)
        {
            npcController.currentTarget = storage;

            npcController.moveController
                .MoveTo(storage.position);
        }
    }
}
