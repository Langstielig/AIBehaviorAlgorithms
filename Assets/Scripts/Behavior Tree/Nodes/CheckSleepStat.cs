using UnityEngine;

public class CheckSleepStat : Node
{
    public override Status Process(NPCController npc, Billboard billboard)
    {
        int energy = npc.stats.energy;
        if(energy > 50)
        {
            Debug.Log("I don't want to sleep, because my energy is " + energy);
            status = Status.Failure;
            return status;
        }

        Debug.Log("I want to sleep because my energy is " + energy);
        status = Status.Success;
        return status;
    }
}
