using UnityEngine;

public class CheckHungerStat : Node
{
    public override Status Process(NPCController npc, Billboard billboard)
    {
        int hunger = npc.stats.hunger;
        if (hunger < 75)
        {
            Debug.Log("I'm not hungry because my hunger is " + hunger);
            status = Status.Failure;
            return status;
        }

        Debug.Log("I'm hungry because my hunger is " + hunger);
        status = Status.Success;
        return status;
    }
}
