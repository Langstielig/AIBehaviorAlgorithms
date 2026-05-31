using UnityEngine;

[CreateAssetMenu(fileName = "DropOffResource", menuName = "UtilityAI/Actions/DropOffResource")]
public class DropOffResource : Action
{
    //public override HFSMAction CreateHFSMAction()
    //{
    //    return new DropOffResourceHFSMAction();
    //}

    public override void Execute(NPCController npc)
    {
        //MyLogger.LogStats(npc, this);
        Debug.Log("DropOffResourceAction");
        //npc.Inventory.RemoveAllResource();
        //npc.stats.money += 20;

        //npc.FinishExecutingBestAction();
        npc.DoDropOffResources();
    }

    public override void SetRequiredDestination(NPCController npc)
    {
        RequiredDestination = npc.context.storage.transform;
        npc.moveController.destination = RequiredDestination;
    }
}
