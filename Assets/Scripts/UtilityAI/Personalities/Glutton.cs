using UnityEngine;

[CreateAssetMenu(fileName = "Glutton", menuName = "UtilityAI/Personalities/Glutton")]
public class Glutton : Personality
{
    public Glutton() 
    {
        hungerMultiplier = 1.4f;
        sleepMultiplier = 1.1f;
    }
}
