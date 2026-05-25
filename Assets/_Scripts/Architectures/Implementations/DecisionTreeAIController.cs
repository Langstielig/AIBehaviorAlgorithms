using UnityEngine;

public class DecisionTreeAIController : IAIController
{
    private NPCController npcController;

    private DTNode rootNode;

    public void Initialize(NPCController npc)
    {
        npcController = npc;

        BuildTree();
    }

    public void Tick()
    {
        ActionNode action =
            rootNode.MakeDecision() as ActionNode;

        action?.Execute();
    }

    private void BuildTree()
    {
        ActionNode sleepNode =
            new SleepActionNode(npcController);

        ActionNode moveHomeNode =
            new GoToHomeActionNode(npcController);

        ActionNode eatNode =
            new EatActionNode(npcController);

        ActionNode moveFoodNode =
            new GoToFoodActionNode(npcController);

        ActionNode dropNode =
            new DropOffResourcesActionNode(npcController);

        ActionNode moveStorageNode =
            new GoToStorageActionNode(npcController);

        ActionNode workNode =
            new WorkActionNode(npcController);

        ActionNode moveResourceNode =
            new GoToWorkActionNode(npcController);

        DecisionNode atResourceDecision =
            new DecisionNode(
                () => npcController.currentTarget != null &&
                Vector3.Distance(
                    npcController.transform.position,
                    npcController.currentTarget.position)
                    <= npcController.minDistance,

                workNode,
                moveResourceNode);

        DecisionNode atStorageDecision =
            new DecisionNode(
                () => npcController.currentTarget != null &&
                Vector3.Distance(
                    npcController.transform.position,
                    npcController.currentTarget.position)
                    <= npcController.minDistance,

                dropNode,
                moveStorageNode);

        DecisionNode inventoryDecision =
            new DecisionNode(
                () => npcController.Inventory
                    .HowFullIsStorage() >= 1f,

                atStorageDecision,
                atResourceDecision);

        DecisionNode atFoodDecision =
            new DecisionNode(
                () => npcController.currentTarget != null &&
                Vector3.Distance(
                    npcController.transform.position,
                    npcController.currentTarget.position)
                    <= npcController.minDistance,

                eatNode,
                moveFoodNode);

        DecisionNode hungerDecision =
            new DecisionNode(
                () => npcController.stats.hunger >= 75,

                atFoodDecision,
                inventoryDecision);

        DecisionNode atHomeDecision =
            new DecisionNode(
                () => npcController.currentTarget != null &&
                Vector3.Distance(
                    npcController.transform.position,
                    npcController.currentTarget.position)
                    <= npcController.minDistance,

                sleepNode,
                moveHomeNode);

        DecisionNode energyDecision =
            new DecisionNode(
                () => npcController.stats.energy <= 50,

                atHomeDecision,
                hungerDecision);

        rootNode = energyDecision;
    }
}
