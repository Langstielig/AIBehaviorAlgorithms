public class UtilityAIController : IAIController
{
    private NPCController npcController;
    public void Initialize(NPCController npc)
    {
        npcController = npc;
    }

    public void Tick()
    {
        npcController.FSMTick();
    }
}
