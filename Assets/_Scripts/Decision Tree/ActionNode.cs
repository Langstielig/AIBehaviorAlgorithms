public abstract class ActionNode : DTNode
{
    protected NPCController npcController;

    public abstract string StateName { get; }

    public ActionNode(NPCController npcController)
    { 
        this.npcController = npcController; 
    }

    public override DTNode MakeDecision()
    {
        return this;
    }

    public abstract void Execute();
}
