using UnityEngine;

[CreateAssetMenu(fileName = "Eat", menuName = "UtilityAI/Actions/Eat")]
public class Eat : Action
{
    //public override HFSMAction CreateHFSMAction()
    //{
    //    return new EatHFSMAction();
    //}

    public override void Execute(NPCController npc)
    {
        //MyLogger.LogStats(npc, this);
        Debug.Log("EatAction");

        //we can manipulating with eating right here because we dont need coroutines
        // Logic for updating everything involved with eating
        //npc.stats.hunger -= 30;
        //npc.stats.money -= 10;

        //npc.FinishExecutingBestAction();
        //npc.onFinishedAction();

        npc.DoEat();
    }

    public override void SetRequiredDestination(NPCController npc)
    {
        //float minDistance = Mathf.Infinity;
        //Transform nearestResource = null;

        //List<Transform> resources = npc.context.Destinations[DestinationType.resource];
        //foreach (Transform resource in resources)
        //{
            //float distanceFromResource = Vector3.Distance(resource.position, npc.transform.position);
            //if (distanceFromResource < minDistance)
            //{
                //minDistance = distanceFromResource;
                //nearestResource = resource;
            //}
        //}


        RequiredDestination = npc.context.FindNearestPosition(DestinationType.food, npc.transform.position);
        npc.moveController.destination = RequiredDestination;
    }
}
