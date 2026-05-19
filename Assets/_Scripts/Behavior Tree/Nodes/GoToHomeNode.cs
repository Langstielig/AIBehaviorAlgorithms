using UnityEngine;

public class GoToHomeNode : Node
{
    public GoToHomeNode(string name) : base(name) { }

    public override Status Process(NPCController npc, Billboard billboard)
    {
        //Debug.Log("I'm going home");

        npc.FindHomePosition();
        Vector3 position = npc.currentTarget.position;
        float distance = Vector3.Distance(npc.transform.position, position);

        if (distance <= npc.minDistance)
        {
            npc.stats.energy = 100;
            status = Status.Success;
            return status;
        }

        npc.moveController.MoveTo(position);
        status = Status.Running;
        if(billboard != null)
        {
            billboard.UpdateBestAction(name);
        }
        return status;
    }
}
