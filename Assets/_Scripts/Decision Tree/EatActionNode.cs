public class EatActionNode : ActionNode
{
    public EatActionNode(
        NPCController npcController)
        : base(npcController)
    {

    }

    public override string StateName => "Eat";

    public override void Execute()
    {
        npcController.billboard.UpdateBestAction(StateName);

        npcController.DoEat();
    }
}
