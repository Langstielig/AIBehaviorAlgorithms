using UnityEngine;

public class GoToFoodActionNode : ActionNode
{
    public GoToFoodActionNode(
        NPCController npcController)
        : base(npcController)
    {

    }

    public override string StateName => "Eat";

    public override void Execute()
    {
        Transform food =
            npcController.context.FindNearestPosition(
                DestinationType.food,
                npcController.transform.position);

        if (food != null)
        {
            npcController.currentTarget = food;

            npcController.moveController
                .MoveTo(food.position);
        }
    }
}
