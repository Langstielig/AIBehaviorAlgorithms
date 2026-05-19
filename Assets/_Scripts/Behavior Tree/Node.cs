using System.Collections.Generic;

public class Node
{
    public enum Status
    {
        Success,
        Running,
        Failure
    }

    public Status status; //protected?

    //public Node parent?
    public List<Node> childrenNodes;
    public int currentChild;
    public string name;

    private Action action;

    //Dictionary for sharing data

    public Node()
    {
        childrenNodes = new List<Node>();
    }

    public Node(string name)
    {
        childrenNodes = new List<Node>();
        this.name = name;
        this.action = null;
    }

    public Node(string name, Action action)
    {
        childrenNodes = new List<Node>();
        this.name = name;
        this.action = action;
    }

    public virtual Status Process(NPCController npc, Billboard billboard)
    {
        if (action != null)
        {
            action.Execute(npc);

            status = Status.Success;
        }
        else
        {
            status = Status.Failure;
        }   
        
        return status; //Status.Success???
    }

    public void AddChild(Node child)
    {
        childrenNodes.Add(child);
    }
}
