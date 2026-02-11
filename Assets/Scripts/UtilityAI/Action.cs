using UnityEngine;

public abstract class Action : ScriptableObject
{
    public string Name;
    private float _score;
    public float score
    {
        get { return _score; }
        set { _score = Mathf.Clamp01(value); }
    }

    public float cost;
    public float distanceCost;
    public float totalCost;

    public Consideration[] considerations;

    public Transform RequiredDestination { get; protected set; }

    private float maxDistance = 22f;

    public virtual void Awake()
    {
        score = 0;
    }

    public abstract void Execute(NPCController npc);

    public abstract void SetRequiredDestination(NPCController npc);

    public virtual float CalculateDynamicCost(NPCController npc)
    {
        if(RequiredDestination == null)
        {
            totalCost = 0;
            return 0;
        }

        float distance = Vector3.Distance(npc.transform.position, RequiredDestination.position);
        float dynamicCost = Mathf.Clamp01(distance / maxDistance);
        distanceCost = dynamicCost;
        totalCost = Mathf.Clamp01(cost + dynamicCost * 0.5f);
        return totalCost;
    }
}
