using UnityEngine;

[CreateAssetMenu(fileName = "MoneyConsideration", menuName = "UtilityAI/Considerations/Money Consideration")]
public class MoneyConsideration : Consideration
{
    [SerializeField] private AnimationCurve responseCurve;
    public override float ScoreConsideration(NPCController npc)
    {
        score = responseCurve.Evaluate(Mathf.Clamp01(npc.stats.money / 1000f));
        return score;
    }
}
