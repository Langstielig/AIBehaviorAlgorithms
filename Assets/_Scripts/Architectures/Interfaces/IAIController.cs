public enum AIType
{
    UtilityAI,
    BehaviorTree
}

public interface IAIController 
{
    void Initialize(NPCController npc);
    void Tick();
}
