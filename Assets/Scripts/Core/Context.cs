using System.Collections.Generic;
using UnityEngine;

public enum DestinationType
{
    rest, 
    resource,
    storage
}

public class Context : MonoBehaviour
{
    public Storage storage;
    public GameObject home;
    public string resourceTag = "resource";
    public float MinDistance = 5f;
    public Dictionary<DestinationType, List<Transform>> Destinations { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        List<Transform> restDestinations = new List<Transform>() { home.transform };
        List<Transform> storageDestinations = new List<Transform> { storage.transform };
        List<Transform> resourceDestinations = GetAllResources();

        Destinations = new Dictionary<DestinationType, List<Transform>>()
        {
            {DestinationType.rest, restDestinations},
            {DestinationType.storage, storageDestinations},
            {DestinationType.resource, resourceDestinations},
        };
    }

    private List<Transform> GetAllResources()
    {
        Transform[] gameObjects = FindObjectsOfType<Transform>() as Transform[];
        List<Transform> resources = new List<Transform>();

        foreach(Transform t in gameObjects)
        {
            if(t.gameObject.tag == resourceTag)
            {
                resources.Add(t);
            }
        }
        return resources;
    }
}
