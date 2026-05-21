public enum AIType
{
    UtilityAI,
    BehaviorTree,
    Hybrid,
    FSM
}

public interface IAIController 
{
    void Initialize(NPCController npc);
    void Tick();
}
