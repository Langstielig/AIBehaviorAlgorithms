using UnityEngine;

public class DestinationPoint : MonoBehaviour
{
    [SerializeField] protected DestinationType destinationType;

    protected virtual void Start()
    {
        Context context = FindAnyObjectByType<Context>();
        context.RegisterDestination(destinationType, transform);
    }
}
