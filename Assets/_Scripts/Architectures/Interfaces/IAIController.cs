public enum AIType
{
    UtilityAI,
    BehaviorTree,
    Hybrid,
    FSM,
    DecisionTree,
    HFSM
}

public interface IAIController 
{
    void Initialize(NPCController npc);
    void Tick();
}
