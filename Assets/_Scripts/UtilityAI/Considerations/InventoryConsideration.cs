using UnityEngine;

[CreateAssetMenu(fileName = "InventoryConsideration", menuName = "UtilityAI/Considerations/Inventory Consideration")]
public class InventoryConsideration : Consideration
{
    [SerializeField] private AnimationCurve responseCurve;

    public override float ScoreConsideration(NPCController npc)
    {
        float score = responseCurve.Evaluate(Mathf.Clamp01(npc.Inventory.HowFullIsStorage()));

        if (npc.withPersonality)
        {
            float multiplier = npc.personality.GetMultiplierForNeed(needType);
            float personalityInfluence = Mathf.Lerp(1f, multiplier, 1 - score);
            //Debug.Log("score before personality: " + score);
            score *= personalityInfluence;
            //Debug.Log("score with personality " + needType + ": " + score);
        }
        else
        {
            //Debug.Log("score Inventory without personality: " + score);
        }

        return score;
    }
}
