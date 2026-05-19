using UnityEngine;

public abstract class Consideration : ScriptableObject
{
    public string Name;
    public NeedType needType;

    private float _score;
    public float score
    {
        get { return _score; }
        set { _score = Mathf.Clamp01(value); }
    }

    public virtual void Awake()
    {
        score = 0;
    }

    public abstract float ScoreConsideration(NPCController npc);
}

public enum NeedType
{
    Hunger,
    Sleep,
    Money,
    Inventory
}