using UnityEngine;

public class CheckInventory : Node
{
    public override Status Process(NPCController npc, Billboard billboard)
    {
        float inventory = npc.Inventory.HowFullIsStorage();
        if (inventory < 1f)
        {
            Debug.Log("I have space in my inventory because it is full at " + inventory);
            status = Status.Failure;
            return status;
        }

        Debug.Log("My inventory is full because it is full at " + inventory);
        status = Status.Success;
        return status;
    }
}
