using UnityEngine;

[CreateAssetMenu(fileName = "DropOffResource", menuName = "UtilityAI/Actions/DropOffResource")]
public class DropOffResource : Action
{
    public override void Execute(NPCController npc)
    {
        //MyLogger.LogStats(npc, this);
        Debug.Log("Drop off resource");
        npc.Inventory.RemoveAllResource();
        npc.stats.money += 20;

        if (npc.isUtilityAI)
        {
            npc.aiBrain.finishedExecutingBestAction = true;
        }
    }

    public override void SetRequiredDestination(NPCController npc)
    {
        RequiredDestination = npc.context.storage.transform;
        npc.moveController.destination = RequiredDestination;
    }
}
