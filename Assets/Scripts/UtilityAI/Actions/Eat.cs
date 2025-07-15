using UnityEngine;

[CreateAssetMenu(fileName = "Eat", menuName = "UtilityAI/Actions/Eat")]
public class Eat : Action
{
    public override void Execute(NPCController npc)
    {
        Debug.Log("I ate food!");

        //we can manipulating with eating right here because we dont need coroutines
        // Logic for updating everything involved with eating
        npc.stats.hunger -= 30;
        npc.stats.money -= 10;

        if (npc.isUtilityAI)
        {
            npc.aiBrain.finishedExecutingBestAction = true;
        }
        //npc.onFinishedAction();
    }

    public override void SetRequiredDestination(NPCController npc)
    {
        RequiredDestination = npc.transform;
    }
}
