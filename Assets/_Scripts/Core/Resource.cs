using UnityEngine;

public enum ResourceType
{
    food,
    stone,
    wood
}

public class Resource : DestinationPoint
{
    [SerializeField] private ResourceType resourceType;

    public ResourceType ResourceType
    {
        get { return resourceType; }
        set { resourceType = value; }
    }

    [SerializeField] private int initialAmount;
    public int InitialAmount
    {
        get { return initialAmount; }
        set { initialAmount = value; }
    }

    [SerializeField] private int amountAvailable;
    public int AmountAvailable
    {
        get { return amountAvailable; }
        set { amountAvailable = value; }
    }

    public delegate void ResourceExhausted();
    public event ResourceExhausted OnResourceExhausted;

    private void Awake()
    {
        destinationType = DestinationType.resource;
    }

    protected override void Start()
    {
        AmountAvailable = InitialAmount;

        base.Start();
        //Context context = FindAnyObjectByType<Context>();
        //context.RegisterDestination(DestinationType.resource, transform);
    }

    public void RemoveAmount(int amountToRemove, NPCController npc)
    {
        if (amountToRemove <= AmountAvailable)
        {
            AmountAvailable -= amountToRemove;
            npc.Inventory.AddResource(resourceType, amountToRemove);
        }

        if(amountToRemove > AmountAvailable)
        {
            npc.Inventory.AddResource(resourceType, AmountAvailable);
            AmountAvailable = 0;
        }

        if(AmountAvailable <= 0)
        {
            OnResourceExhausted?.Invoke();
            Destroy(gameObject);
        }
    }
}
