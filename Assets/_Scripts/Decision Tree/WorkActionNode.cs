using UnityEngine;

public class WorkActionNode : ActionNode
{
    public WorkActionNode(
        NPCController npcController)
        : base(npcController)
    {

    }

    public override string StateName => "Work";

    public override void Execute()
    {
        npcController.billboard.UpdateBestAction(StateName);

        npcController.DoWork();
    }
}
