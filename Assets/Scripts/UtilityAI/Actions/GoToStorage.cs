using UnityEngine;

[CreateAssetMenu(fileName = "GoToStorage", menuName = "UtilityAI/Actions/GoToStorage")]
public class GoToStorage : Action
{
    public override void Execute(NPCController npc)
    {
        //MyLogger.LogStats(npc, this);
        Debug.Log("I'm going to storage");

        Vector3 position = npc.FindStoragePosition();
        npc.moveController.MoveTo(position);
    }

    public override void SetRequiredDestination(NPCController npc)
    {
        RequiredDestination = npc.transform;
    }
}
