using UnityEngine;

public class GoToWorkNode : Node
{
    public GoToWorkNode(string name) : base(name) { }

    public override Status Process(NPCController npc, Billboard billboard)
    {
        Debug.Log("I'm going to work");

        Vector3 position = npc.FindWorkPosition();
        float distance = Vector3.Distance(npc.transform.position, position);

        if (distance <= npc.minDistance)
        {
            npc.Inventory.AddResource(ResourceType.wood, 10);
            status = Status.Success;
            return status;
        }

        npc.moveController.MoveTo(position);
        status = Status.Running;
        billboard.UpdateBestAction(name);
        return status;
    }
}
