using UnityEngine;

[CreateAssetMenu(fileName = "MoneyConsideration", menuName = "UtilityAI/Considerations/Money Consideration")]
public class MoneyConsideration : Consideration
{
    [SerializeField] private AnimationCurve responseCurve;
    public override float ScoreConsideration(NPCController npc)
    {
        score = responseCurve.Evaluate(Mathf.Clamp01(npc.stats.money / 1000f));

        if (npc.withPersonality)
        {
            float multiplier = npc.personality.GetMultiplierForNeed(needType);
            float personalityInfluence = Mathf.Lerp(1f, multiplier, 1 - score);
            Debug.Log("score before personality: " + score);
            score *= personalityInfluence;
            Debug.Log("score with personality " + needType + ": " + score);
        }
        else
        {
            Debug.Log("score Money without personality: " + score);
        }

        return score;
    }
}
