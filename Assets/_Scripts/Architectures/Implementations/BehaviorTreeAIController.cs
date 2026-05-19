public class BehaviorTreeAIController : IAIController
{
    private BehaviorTree behaviorTree;

    public void Initialize(NPCController npc)
    {
        behaviorTree = npc.GetComponent<BehaviorTree>();
    }

    public void Tick()
    {
        behaviorTree.ProcessTree();
    }
}
