public class SleepActionNode : ActionNode
{
    public SleepActionNode(
        NPCController npcController)
        : base(npcController)
    {

    }

    public override string StateName => "Sleep";

    public override void Execute()
    {
        npcController.billboard.UpdateBestAction(StateName);

        npcController.DoSleep();
    }
}
