using UnityEngine;

[CreateAssetMenu(fileName = "InventoryConsideration", menuName = "UtilityAI/Considerations/Inventory Consideration")]
public class InventoryConsideration : Consideration
{
    [SerializeField] private AnimationCurve responseCurve;

    public override float ScoreConsideration(NPCController npc)
    {
        float score = responseCurve.Evaluate(Mathf.Clamp01(npc.Inventory.HowFullIsStorage()));
        return score;
    }
}
