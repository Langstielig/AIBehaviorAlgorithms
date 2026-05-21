public class HybridAIController : IAIController
{
    private NPCController npcController;

    public void Initialize(NPCController npc)
    {
        npcController = npc;
        npcController.currentState = State.decide;
    }

    public void Tick()
    {
        npcController.HybridTick();
    }
}
