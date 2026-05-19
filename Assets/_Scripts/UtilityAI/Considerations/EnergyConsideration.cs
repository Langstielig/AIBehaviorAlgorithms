using UnityEngine;

[CreateAssetMenu(fileName = "EnergyConsideration", menuName = "UtilityAI/Considerations/Energy Consideration")]
public class EnergyConsideration : Consideration
{
    [SerializeField] private AnimationCurve responseCurve;
    public override float ScoreConsideration(NPCController npc)
    {
        score = responseCurve.Evaluate(Mathf.Clamp01(npc.stats.energy / 100f));

        if(npc.withPersonality)
        {
            float multiplier = npc.personality.GetMultiplierForNeed(needType);
            float personalityInfluence = Mathf.Lerp(1f, multiplier, 1 - score);
            //Debug.Log("score before personality: " + score);
            score *= personalityInfluence;
            //Debug.Log("score with personality " + needType + ": " + score);
        }
        else
        {
            //Debug.Log("score Energy without personality: " + score);
        }

        return score;
    }
}
