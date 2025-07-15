using UnityEngine;

[CreateAssetMenu(fileName = "GoToHome", menuName = "UtilityAI/Actions/GoToHome")]
public class GoToHome : Action
{
    public override void Execute(NPCController npc)
    {
        Debug.Log("I'm going home");

        Vector3 position = npc.FindHomePosition();
        npc.moveController.MoveTo(position);
    }

    public override void SetRequiredDestination(NPCController npc)
    {
        RequiredDestination = npc.transform;
    }
}
