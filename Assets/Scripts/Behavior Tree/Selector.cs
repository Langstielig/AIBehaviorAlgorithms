public class Selector : Node
{
    public Selector() : base() { }

    public Selector(string name) : base(name) { }

    public override Status Process(NPCController npc, Billboard billboard)
    {
        foreach(Node node in childrenNodes)
        {
            switch(node.Process(npc, billboard))
            {
                case Status.Failure:
                    continue;
                case Status.Success:
                    status = Status.Success;
                    return status;
                case Status.Running:
                    status = Status.Running;
                    return status;
                default:
                    continue;
            }
        }

        status = Status.Failure;
        return status;
    }
}
