using UnityEngine;

public class WorkNode : Node
{
    public override Status Process(NPCController npc, Billboard billboard)
    {
        Debug.Log("I'm working");

        npc.DoWork();

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
