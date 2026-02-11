using UnityEngine;

[CreateAssetMenu(fileName = "HungerConsideration", menuName = "UtilityAI/Considerations/Hunger Consideration")]
public class HungerConsideration : Consideration
{
    [SerializeField] private AnimationCurve responseCurve;
    public override float ScoreConsideration(NPCController npc)
    {
        score = responseCurve.Evaluate(Mathf.Clamp01(npc.stats.hunger / 100f));

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
            Debug.Log("score Hunger without personality: " + score);
        }

        return score;
    }
}
