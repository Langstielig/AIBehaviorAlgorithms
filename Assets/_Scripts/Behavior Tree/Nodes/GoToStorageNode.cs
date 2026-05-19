using UnityEngine;

public class GoToStorageNode : Node
{
    public GoToStorageNode(string name) : base(name) { }

    public override Status Process(NPCController npc, Billboard billboard)
    {
        Debug.Log("I'm going to storage");

        npc.FindStoragePosition();
        Vector3 position = npc.currentTarget.position;
        float distance = Vector3.Distance(npc.transform.position, position);

        if (distance <= npc.minDistance)
        {
            npc.Inventory.RemoveAllResource();
            npc.stats.money += 20;

            status = Status.Success;
            return status;
        }

        npc.moveController.MoveTo(position);
        status = Status.Running;
        billboard.UpdateBestAction(name);
        return status;
    }
}
