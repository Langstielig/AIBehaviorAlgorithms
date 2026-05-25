public abstract class HFSMAction
{
    public abstract string StateName { get; }

    public abstract void Enter(NPCController npcController);
    public abstract void Tick(NPCController npcController);
    public abstract bool IsFinished();
    public abstract void Exit(NPCController npcController);
}
