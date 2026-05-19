using UnityEngine;

public class Storage : StorageInventory
{
    [SerializeField] private int maxCapacityPerType;

    void Start()
    {
        InitializeInventory();
        maxCapacityPerType = maxCapacityPerType * Inventory.Count;
    }

    public void SetMaxCapacityPerType(int capacity)
    {
        maxCapacityPerType = capacity;
    }

    public override void AddResource(ResourceType resourceType, int amount)
    {
        int amountInInventory = Inventory[resourceType];
        if (amountInInventory + amount > maxCapacityPerType)
        {
            int amountCanAdd = maxCapacityPerType - amountInInventory;
            Inventory[resourceType] += amountCanAdd;
        }
        else
        {
            Inventory[resourceType] += amount;
        }
    }

    public override void RemoveResource(ResourceType resourceType, int amount)
    {
        if (Inventory[resourceType] - amount < 0)
        {
            Inventory[resourceType] = 0;
        }
        else
        {
            Inventory[resourceType] -= amount;
        }
    }
}
