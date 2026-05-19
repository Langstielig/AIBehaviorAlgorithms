public class Sequence : Node
{
    private int currentNodeIndex = 0;

    public Sequence() : base() { }

    public Sequence(string name) : base(name) { }

    public override Status Process(NPCController npc, Billboard billboard)
    {
        bool anyChildIsRunning = false;

        while(currentNodeIndex < childrenNodes.Count)
        {
            Status childStatus = childrenNodes[currentNodeIndex].Process(npc, billboard);

            switch (childStatus)
            {
                case Status.Failure:
                    currentNodeIndex = 0;
                    status = Status.Failure;
                    return status;
                case Status.Success:
                    currentNodeIndex++;
                    break;
                case Status.Running:
                    status = Status.Running;
                    return status;
                default:
                    status = Status.Success;
                    return status;
            }
        }

        currentNodeIndex = 0;
        status = Status.Success;
        return status;
    }
}
