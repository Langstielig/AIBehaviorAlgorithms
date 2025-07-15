using UnityEngine;

[CreateAssetMenu(fileName = "GoToWork", menuName = "UtilityAI/Actions/GoToWork")]
public class GoToWork : Action
{
    public override void Execute(NPCController npc)
    {
        Debug.Log("I'm going to work");

        Vector3 destination = npc.FindWorkPosition();
        npc.moveController.MoveTo(destination);
    }

    public override void SetRequiredDestination(NPCController npc)
    {
        RequiredDestination = npc.transform;
    }
}
