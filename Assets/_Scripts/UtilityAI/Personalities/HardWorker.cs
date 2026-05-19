using UnityEngine;

[CreateAssetMenu(fileName = "HardWorker", menuName = "UtilityAI/Personalities/HardWorker")]
public class HardWorker : Personality
{
    //“рудоголик медленнее устает и любит работать

    public HardWorker()
    {
        hungerMultiplier = 0.8f;
        sleepMultiplier = 0.8f;
        moneyMotivation = 1.5f;
    }
}
