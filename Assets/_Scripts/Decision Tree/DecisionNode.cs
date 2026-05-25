using System;

public class DecisionNode : DTNode
{
    private Func<bool> condition;

    private DTNode trueNode;
    private DTNode falseNode;

    public DecisionNode(Func<bool> condition, DTNode trueNode, DTNode falseNode)
    {
        this.condition = condition;
        this.trueNode = trueNode;
        this.falseNode = falseNode;
    }

    public override DTNode MakeDecision()
    {
        if (condition())
        {
            return trueNode.MakeDecision();
        }
        else
        {
            return falseNode.MakeDecision();
        }
    }
}
