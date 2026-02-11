using UnityEngine;

public class Personality : ScriptableObject
{
    public string Name;

    public float hungerMultiplier = 1f;
    public float sleepMultiplier = 1f;
    public float moneyMotivation = 1f;
    public float inventoryMotivation = 1f;

    public float GetMultiplierForNeed(NeedType needType)
    {
        switch (needType)
        {
            case NeedType.Hunger: return hungerMultiplier;
            case NeedType.Sleep: return sleepMultiplier;
            case NeedType.Money: return moneyMotivation;
            case NeedType.Inventory: return inventoryMotivation;
            default: return 1f;
        }
    }
}
