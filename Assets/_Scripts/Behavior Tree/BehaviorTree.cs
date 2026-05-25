using UnityEngine;

//public class BehaviorTree : Node
public class BehaviorTree: MonoBehaviour
{
    private Node root = null;

    [SerializeField] private Billboard billboard;

    [SerializeField] private Action eatAction;
    //[SerializeField] private Action goToStorageAction;
    //[SerializeField] private Action dropOffResourceAction;

    private NPCController npc;

    protected void Start()
    {
        npc = GetComponent<NPCController>();

        //??
        //if (!npc.isUtilityAI)
        //{
            root = SetupTree();
        //}
    }

    //private void Update()
    //{
    //    if(root != null && !npc.isUtilityAI)
    //    {
    //        root.Process(npc, billboard);
    //    }
    //}

    public void ProcessTree()
    {
        if(root != null)
        {
            root.Process(npc, billboard);
        }
    }

    protected Node SetupTree()
    {
        //Node root = new WorkNode(workAction);
        //Node root = new GoToWorkNode(goToWorkAction);

        //Node root = new Node("Go to work", goToWorkAction);

        Node root = new Selector("root");

        //First priority - sleep
        Node sleepSequence = new Sequence("Sleep sequence");

        Node checkSleepStat = new CheckSleepStat();
        Node goToHome = new GoToHomeNode("Sleep");
        Node sleep = new SleepNode();

        sleepSequence.AddChild(checkSleepStat);
        sleepSequence.AddChild(goToHome);
        //sleepSequence.AddChild(sleep);
        root.AddChild(sleepSequence);

        //Second priority - eat
        Node eatSequence = new Sequence("Eat sequence");

        Node checkHungerStat = new CheckHungerStat();
        //EatNode?
        Node eat = new Node("Eat", eatAction);

        eatSequence.AddChild(checkHungerStat);
        eatSequence.AddChild(eat);
        root.AddChild(eatSequence);

        //Third priority - drop off resources
        Node dropOffResourcesSequence = new Sequence("Drop off resource sequence");

        Node checkInventory = new CheckInventory();
        Node goToStorage = new GoToStorageNode("Drop off resources");
        //DropRecourcesNode?
        //Node dropOffResources = new Node("Drop off recources", dropOffResourceAction);

        dropOffResourcesSequence.AddChild(checkInventory);
        dropOffResourcesSequence.AddChild(goToStorage);
        //dropOffResourcesSequence.AddChild(dropOffResources);
        root.AddChild(dropOffResourcesSequence);

        //Fourth priority - work
        Node workSequence = new Sequence("Work sequence");

        Node goToWork = new GoToWorkNode("Work");
        //Node work = new WorkNode();

        workSequence.AddChild(goToWork);
       // workSequence.AddChild(work);
        root.AddChild(workSequence);

        return root;
    }

    //public BehaviorTree()
    //{
    //    name = "Tree";
    //}

    //public BehaviorTree(string name)
    //{
    //    this.name = name;
    //}

    //public override Status Process()
    //{
    //    return childrenNodes[currentChild].Process();
    //}

    //struct NodeLevel
    //{
    //    public int level;
    //    public Node node;
    //}

    //public void PrintTree()
    //{
    //    string result = "";
    //    Stack<NodeLevel> nodeStack = new Stack<NodeLevel>();
    //    Node startNode = root;
    //    nodeStack.Push(new NodeLevel { level = 0, node = startNode });

    //    while(nodeStack.Count != 0)
    //    {
    //        NodeLevel nextNode = nodeStack.Pop();

    //        result += new string('-', nextNode.level) + nextNode.node.name + "\n";

    //        for(int i = nextNode.node.childrenNodes.Count - 1; i >= 0; i--)
    //        {
    //            nodeStack.Push(new NodeLevel { level = nextNode.level + 1, node = nextNode.node.childrenNodes[i] });
    //        }
    //    }

    //    Debug.Log(result);
    //}
}
