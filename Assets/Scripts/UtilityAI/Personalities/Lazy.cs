using UnityEngine;

[CreateAssetMenu(fileName = "Lazy", menuName = "UtilityAI/Personalities/Lazy")]
public class Lazy : Personality
{
    //Ленивый быстрее устает и не любит работать

    public Lazy()
    {
        hungerMultiplier = 1.1f;
        sleepMultiplier = 1.5f;
        moneyMotivation = 0.7f;
    }
}
