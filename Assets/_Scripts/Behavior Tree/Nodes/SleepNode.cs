using UnityEngine;

public class SleepNode : Node
{
    public override Status Process(NPCController npc, Billboard billboard)
    {
        Debug.Log("I'm sleeping");

        npc.DoSleep();

        if (npc.isFinishedActing)
        {
            status = Status.Success;
            return status;
        }

        status = Status.Running;
        billboard.UpdateBestAction(name);
        return status;
    }
}
