using UnityEngine;

public class Leaf : Node
{
    public delegate Status Tick();
    public Tick ProcessMethod;

    public Leaf()
    {

    }

    public Leaf(string name, Tick processMethod)
    {
        this.name = name;
        ProcessMethod = processMethod;
    }

    public override Status Process(NPCController npc, Billboard billboard)
    {
        if (ProcessMethod != null)
        {
            return ProcessMethod();
        }
        else
        {
            return Status.Failure;
        }
    }
}
