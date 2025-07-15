using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Work", menuName = "UtilityAI/Actions/Work")]
public class Work : Action
{
    public override void Execute(NPCController npc)
    {
        Debug.Log("I'm working");

        npc.DoWork();
    }

    public override void SetRequiredDestination(NPCController npc)
    {
        float minDistance = Mathf.Infinity;
        Transform nearestResource = null;

        List<Transform> resources = npc.context.Destinations[DestinationType.resource];
        foreach (Transform resource in resources)
        {
            float distanceFromResource = Vector3.Distance(resource.position, npc.transform.position);
            if(distanceFromResource < minDistance)
            {
                minDistance = distanceFromResource;
                nearestResource = resource;
            }
        }

        RequiredDestination = nearestResource;
        npc.moveController.destination = RequiredDestination;
    }

    //this is a dependencies injection pattern
}
