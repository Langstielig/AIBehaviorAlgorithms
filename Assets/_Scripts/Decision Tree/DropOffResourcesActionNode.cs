using UnityEngine;

public class DropOffResourcesActionNode : ActionNode
{
    public DropOffResourcesActionNode(
        NPCController npcController)
        : base(npcController)
    {

    }

    public override string StateName => "Drop off resources";

    public override void Execute()
    {
        npcController.billboard.UpdateBestAction(StateName);

        npcController.DoDropOffResources();
    }
}
