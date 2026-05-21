using System.Collections.Generic;
using UnityEngine;

public enum DestinationType
{
    rest, 
    resource,
    storage,
    food
}

public class Context : MonoBehaviour
{
    public Storage storage;
    public GameObject home;
    public float MinDistance = 5f;
    public Dictionary<DestinationType, List<Transform>> Destinations { get; private set; }

    private void Awake()
    {
        Destinations = new Dictionary<DestinationType, List<Transform>>()
        {
            {DestinationType.rest, new List<Transform>()},
            {DestinationType.storage, new List<Transform>()},
            {DestinationType.resource, new List <Transform>()},
            {DestinationType.food, new List<Transform>()},
        };

        RegisterDestination(DestinationType.rest, home.transform);
        RegisterDestination(DestinationType.storage, storage.transform);
    }

    //private void Start()
    //{
    //    RegisterDestination(DestinationType.rest, home.transform);
    //    RegisterDestination(DestinationType.storage, storage.transform);
    //}

    public void RegisterDestination(DestinationType destinationType, Transform target)
    {
        if(!Destinations.ContainsKey(destinationType))
        {
            Destinations[destinationType] = new List<Transform>();
        }

        Destinations[destinationType].Add(target);
    }

    public Transform FindNearestPosition(DestinationType destinationType, Vector3 npcPosition)
    {
        float minDistance = Mathf.Infinity;
        Transform nearestPoint = null;

        // TODO: is'n working with NPCSpawner
        List<Transform> destinationPoints = Destinations[destinationType];
        foreach (Transform point in destinationPoints)
        {
            float distance = Vector3.Distance(point.position, npcPosition);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestPoint = point;
            }
        }

        return nearestPoint;
    }
}
